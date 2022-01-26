using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;
using MartianRobots.Logic.Services;
using MartianRobots.Models.Constants;
using MartianRobots.Logic.Validators;

namespace MartianRobots.Logic.Manager
{
    public class RectangularRobotManager : IRobotManager
    {
        private readonly ICommandExecuter<RectangularMoveCommand> cmdExecuter;
        private readonly IPositionValidator positionValidator;
        private Grid Grid { get; set; }
        private List<Robot> Robots { get; set; }
        private List<RobotCommands> RobotCommands { get; set; }
        private List<Position> EdgePositions {get; set;} = new List<Position>();

        public RectangularRobotManager(
            ICommandExecuter<RectangularMoveCommand> cmdExecuter,
            IPositionValidator positionValidator
            )
        {
            this.cmdExecuter = cmdExecuter;
            this.positionValidator = positionValidator;
        }

        public void AssignRobots(Grid grid, List<Robot> robots, List<RobotCommands> robotCommands)
        {
            Grid = grid;
            Robots = robots;
            RobotCommands = robotCommands;
        }

        public void ExecuteTasks()
        {
            foreach (Robot robot in Robots)
            {
                var commands = RobotCommands.FirstOrDefault(c => c.Id == robot.Id).Commands;
                ExecuteRobotTasks(robot, commands);
            }
        }

        private void ExecuteRobotTasks(Robot robot, List<RectangularMoveCommand> commands)
        {
            if (Grid is null) throw new Exception("The grid was not provided");

            GridPosition robotPosition = robot.Position;

            foreach (var command in commands)
            {
                var nextPosition = cmdExecuter.Execute(robotPosition, command);
                
                if (IsMoveCommand(command) && positionValidator.IsRobotOffGrid(nextPosition, Grid))
                {
                    if (positionValidator.IsRobotLost(robotPosition, EdgePositions))
                    {
                        //call to MongoDB
                        robot.IsLost = true;
                        robot.Position = robotPosition;

                        EdgePositions.Add(robotPosition);
                        return;
                    }

                    //call to MongoDB
                    continue;
                }

                //call to MongoDB
                robotPosition = nextPosition;
            }
            robot.Position = robotPosition;
        }

        private static bool IsMoveCommand(RectangularMoveCommand command)
        {
            return command != RectangularMoveCommand.Left && command != RectangularMoveCommand.Right;
        }
    }
}
