using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;
using MartianRobots.Logic.Services;
using MartianRobots.Models.Constants;
using MartianRobots.Logic.Validators;
using MartianRobots.Data.Repositories;
using MartianRobots.Data.Entities;

namespace MartianRobots.Logic.Manager
{
    public class RectangularRobotManager : IRobotManager
    {
        private readonly ICommandExecuter<RectangularMoveCommand> cmdExecuter;
        private readonly IPositionValidator positionValidator;
        private readonly IRobotStepWriteRepository writeRobotRepository;
        private readonly ISavedGridWriteRepository writeGridRepository;

        private Grid Grid { get; set; }
        private List<Robot> Robots { get; set; }
        private List<RobotCommands> RobotCommands { get; set; }
        private List<Position> EdgePositions {get; set;} = new List<Position>();

        public RectangularRobotManager(
            ICommandExecuter<RectangularMoveCommand> cmdExecuter,
            IPositionValidator positionValidator,
            IRobotStepWriteRepository writeRobotRepository,
            ISavedGridWriteRepository writeGridRepository
            )
        {
            this.cmdExecuter = cmdExecuter;
            this.positionValidator = positionValidator;
            this.writeRobotRepository = writeRobotRepository;
            this.writeGridRepository = writeGridRepository;
        }

        public void AssignRobots(Grid grid, List<Robot> robots, List<RobotCommands> robotCommands)
        {
            Grid = grid;
            Robots = robots;
            RobotCommands = robotCommands;
        }

        public async Task<Guid> ExecuteTasksAsync()
        {
            if (Grid is null || Robots is null || RobotCommands is null) throw new Exception("The data was not provided");

            var runId = Guid.NewGuid();
            await SaveGridAsync(runId, Grid);

            foreach (Robot robot in Robots)
            {
                var commands = RobotCommands.FirstOrDefault(c => c.Id == robot.Id).Commands;
                await ExecuteRobotTasks(robot, commands, runId);
            }

            return runId;
        }

        private async Task ExecuteRobotTasks(Robot robot, List<RectangularMoveCommand> commands, Guid runId)
        {
            GridPosition robotPosition = robot.Position;

            foreach (var command in commands)
            {
                await SaveRobotDataAsync(runId, robot.Id, robotPosition, command);
                var nextPosition = cmdExecuter.Execute(robotPosition, command);
                
                if (IsMoveCommand(command) && positionValidator.IsRobotOffGrid(nextPosition, Grid))
                {
                    if (positionValidator.IsRobotLost(robotPosition, EdgePositions))
                    {
                        robot.IsLost = true;
                        robot.Position = robotPosition;

                        EdgePositions.Add(robotPosition);

                        await SaveRobotDataAsync(runId, robot.Id, robotPosition, command, true);
                        return;
                    }

                    await SaveRobotDataAsync(runId, robot.Id, robotPosition, command);
                    continue;
                }

                robotPosition = nextPosition;
            }
            robot.Position = robotPosition;
            await SaveRobotDataAsync(runId, robot.Id, robotPosition);
        }

        private static bool IsMoveCommand(RectangularMoveCommand command)
        {
            return command != RectangularMoveCommand.Left && command != RectangularMoveCommand.Right;
        }

        private async Task SaveRobotDataAsync(Guid runId, int robotId, GridPosition position, RectangularMoveCommand? command = null, bool isLost = false)
        {
            await writeRobotRepository.SaveRobotStepAsync(CreateRobotStep(runId, robotId, position, isLost, command));
        }

        private static RobotStep CreateRobotStep(Guid runId, int robotId, GridPosition position, bool isLost, RectangularMoveCommand? command)
        {
            return new RobotStep
            {
                RunId = runId,
                RobotId = robotId,
                Position = position,
                IsLost = isLost,
                Command = command is not null ? command.ToString() : string.Empty,
            };
        }

        private async Task SaveGridAsync(Guid runId, Grid grid)
        {
            await writeGridRepository.SaveGridAsync(CreateSavedGrid(runId, grid));
        }

        private static SavedGrid CreateSavedGrid(Guid runId, Grid grid)
        {
            return new SavedGrid
            {
                RunId = runId,
                X = grid.X,
                Y = grid.Y,
            };
        }
    }
}
