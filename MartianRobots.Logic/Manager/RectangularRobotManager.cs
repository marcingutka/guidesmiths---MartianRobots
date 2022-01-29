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
        private readonly IDataSetWriteRepository writeDataSetRepository;

        private Grid Grid { get; set; }
        private IEnumerable<Robot> Robots { get; set; }
        private IEnumerable<RobotCommands> RobotCommands { get; set; }
        private string RunName { get; set; }
        private List<Position> EdgePositions {get; set;} = new List<Position>();

        public RectangularRobotManager(
            ICommandExecuter<RectangularMoveCommand> cmdExecuter,
            IPositionValidator positionValidator,
            IRobotStepWriteRepository writeRobotRepository,
            ISavedGridWriteRepository writeGridRepository,
            IDataSetWriteRepository writeDataSetRepository
            )
        {
            this.cmdExecuter = cmdExecuter;
            this.positionValidator = positionValidator;
            this.writeRobotRepository = writeRobotRepository;
            this.writeGridRepository = writeGridRepository;
            this.writeDataSetRepository = writeDataSetRepository;
        }

        public void AssignGridAndRobots(Grid grid, IEnumerable<Robot> robots, IEnumerable<RobotCommands> robotCommands, string runName)
        {
            Grid = grid;
            Robots = robots;
            RobotCommands = robotCommands;
            RunName = runName;
        }

        public async Task<Guid> ExecuteTasksAsync()
        {
            if (Grid is null || Robots is null || RobotCommands is null) throw new Exception("The data was not provided");

            var runId = Guid.NewGuid();
            await SaveGridAsync(runId, Grid);

            foreach (Robot robot in Robots)
            {
                var metrics = new List<RobotStep>();
                var commands = RobotCommands.FirstOrDefault(c => c.Id == robot.Id).Commands;
                ExecuteRobotTasks(robot, commands, runId, metrics);
                await SaveRobotDataAsync(metrics);
            }

            await writeDataSetRepository.SaveNameAsync(new DataSet { RunId = runId, Name = RunName, GenerationDate = DateTime.UtcNow });
            return runId;
        }

        private void ExecuteRobotTasks(Robot robot, List<RectangularMoveCommand> commands, Guid runId, List<RobotStep> metrics)
        {
            GridPosition robotPosition = robot.Position;
            int stepNo = 0;

            foreach (var command in commands)
            {
                stepNo++;
                metrics.Add(CollectMetricData(runId, robot.Id, stepNo, robotPosition, command));
                var nextPosition = cmdExecuter.Execute(robotPosition, command);
                
                if (IsMoveCommand(command) && positionValidator.IsRobotOffGrid(nextPosition, Grid))
                {
                    if (positionValidator.IsRobotLost(robotPosition, EdgePositions))
                    {
                        robot.IsLost = true;
                        robot.Position = robotPosition;

                        EdgePositions.Add(robotPosition);

                        metrics.Add(CollectMetricData(runId, robot.Id, stepNo, robotPosition, command, true));
                        return;
                    }

                    metrics.Add(CollectMetricData(runId, robot.Id, stepNo, robotPosition, command));
                    continue;
                }

                robotPosition = nextPosition;
            }
            robot.Position = robotPosition;

            metrics.Add(CollectMetricData(runId, robot.Id, stepNo, robotPosition));            
        }

        private static bool IsMoveCommand(RectangularMoveCommand command)
        {
            return command != RectangularMoveCommand.Left && command != RectangularMoveCommand.Right;
        }

        private static RobotStep CollectMetricData(Guid runId, int robotId, int stepNo, GridPosition position, RectangularMoveCommand? command = null, bool isLost = false)
        {
            return new RobotStep
            {
                RunId = runId,
                RobotId = robotId,
                StepNumber = stepNo,
                Position = new Position { X = position.X, Y = position.Y },
                Orientation = position.Orientation,
                IsLost = isLost,
                Command = command is not null ? command.ToString() : string.Empty,
            };
        }

        private async Task SaveRobotDataAsync(List<RobotStep> robotPositions)
        {
            await writeRobotRepository.SaveRobotMovement(robotPositions);
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
