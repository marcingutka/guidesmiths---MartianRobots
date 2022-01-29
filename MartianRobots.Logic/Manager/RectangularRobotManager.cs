using MartianRobots.Models;
using MartianRobots.Logic.Services;
using MartianRobots.Models.Constants;
using MartianRobots.Logic.Validators;
using MartianRobots.Data.Entities;

namespace MartianRobots.Logic.Manager
{
    public class RectangularRobotManager : IRobotManager
    {
        private readonly ICommandExecuter<RectangularMoveCommand> cmdExecuter;
        private readonly IPositionValidator positionValidator;
        private readonly IDataTracker dataTracker;

        private Grid Grid { get; set; }
        private IEnumerable<Robot> Robots { get; set; }
        private IEnumerable<RobotCommands> RobotCommands { get; set; }
        private string RunName { get; set; }
        private List<Position> EdgePositions {get; set;} = new ();

        public RectangularRobotManager(
            ICommandExecuter<RectangularMoveCommand> cmdExecuter,
            IPositionValidator positionValidator,
            IDataTracker dataTracker
            )
        {
            this.cmdExecuter = cmdExecuter;
            this.positionValidator = positionValidator;
            this.dataTracker = dataTracker;
        }

        public void AssignGridAndRobots(Grid grid, IEnumerable<Robot> robots, IEnumerable<RobotCommands> robotCommands, string runName)
        {
            Grid = grid;
            Robots = robots;
            RobotCommands = robotCommands;
            RunName = runName;
        }

        public async Task ExecuteTasksAsync()
        {
            if (Grid is null || Robots is null || RobotCommands is null) throw new Exception("The data was not provided");

            await dataTracker.SaveGridAsync(Grid);

            foreach (Robot robot in Robots)
            {
                var commands = RobotCommands.FirstOrDefault(c => c.Id == robot.Id).Commands;
                ExecuteRobotTasks(robot, commands);
                await dataTracker.SaveRobotDataAsync();
            }

            await dataTracker.SaveRunNameAsync(RunName, DateTime.UtcNow);
        }

        private void ExecuteRobotTasks(Robot robot, List<RectangularMoveCommand> commands)
        {
            GridPosition robotPosition = robot.Position;
            int stepNo = 0;

            foreach (var command in commands)
            {
                stepNo++;
                dataTracker.CollectMetricData(robot.Id, stepNo, robotPosition, command);
                var nextPosition = cmdExecuter.Execute(robotPosition, command);
                
                if (IsMoveCommand(command) && positionValidator.IsRobotOffGrid(nextPosition, Grid))
                {
                    if (positionValidator.IsRobotLost(robotPosition, EdgePositions))
                    {
                        robot.IsLost = true;
                        robot.Position = robotPosition;

                        EdgePositions.Add(robotPosition);

                        dataTracker.CollectMetricData(robot.Id, stepNo, robotPosition, command, true);
                        return;
                    }

                    dataTracker.CollectMetricData(robot.Id, stepNo, robotPosition, command);
                    continue;
                }

                robotPosition = nextPosition;
            }
            robot.Position = robotPosition;

            dataTracker.CollectMetricData(robot.Id, stepNo, robotPosition);            
        }

        private static bool IsMoveCommand(RectangularMoveCommand command)
        {
            return command != RectangularMoveCommand.Left && command != RectangularMoveCommand.Right;
        }
    }
}
