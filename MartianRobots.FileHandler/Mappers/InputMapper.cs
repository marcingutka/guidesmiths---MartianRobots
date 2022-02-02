using System.ComponentModel.DataAnnotations;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using MartianRobots.FileHandler.Validator;

namespace MartianRobots.FileHandler.Mappers
{
    public class InputMapper : IInputMapper
    {
        private readonly IInputValidator validator;
        public InputMapper(IInputValidator validator)
        {
            this.validator = validator;
        }

        public (Grid Grid, IEnumerable<Robot> Robots, IEnumerable<RobotCommands> Commands) Map(List<string> data)
        {
            Grid grid;
            (IEnumerable<Robot>, IEnumerable<RobotCommands>) robots;

            try
            {
                validator.Validate(data);            

                grid = MapGrid(data[0]);

                robots = MapRobots(data.Skip(1).ToList());

                validator.CheckIfEachRobotStartsOnGrid(robots.Item1.ToList(), grid);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }

            return (grid, robots.Item1, robots.Item2);
        }

        private static Grid MapGrid(string data)
        {
            var lineSplit = data.Split(" ");

            return new Grid(int.Parse(lineSplit[0]), int.Parse(lineSplit[1]));
        }

        private static (IEnumerable<Robot>, IEnumerable<RobotCommands>) MapRobots(List<string> data)
        {
            var robotList = new List<Robot>();
            var robotsCommands = new List<RobotCommands>();

            for (int i = 0; i < data.Count; i += 2)
            {
                var robot = MapRobot(data[i], i);

                var commandList = MapCommands(data[i + 1]);

                var robotCommands = new RobotCommands
                {
                    Id = robot.Id,
                    Commands = GetMoveEnums(commandList)
                };

                robotList.Add(robot);
                robotsCommands.Add(robotCommands);
            }

            return (robotList, robotsCommands);
        }

        private static Robot MapRobot(string robotPosition, int enumerator)
        {
            var lineSplit = robotPosition.Split(" ");

            var robot = new Robot()
            {
                Id = enumerator / 2 + 1,
                Position = new GridPosition()
            };

            robot.Position.X = int.Parse(lineSplit[0]);
            robot.Position.Y = int.Parse(lineSplit[1]);
            robot.Position.Orientation = GetOrientationEnum(lineSplit[2]);

            return robot;
        }

        private static List<char> MapCommands(string commands)
        {
            var commandList = new List<char>();

            foreach (var command in commands)
            {
                commandList.Add(command);
            }

            return commandList;
        }

        private static List<RectangularMoveCommand> GetMoveEnums(List<char> commands)
        {
            var list = new List<RectangularMoveCommand>();
            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'L':
                        list.Add(RectangularMoveCommand.Left);
                        break;

                    case 'R':
                        list.Add(RectangularMoveCommand.Right);
                        break;

                    case 'F':
                        list.Add(RectangularMoveCommand.Forward);
                        break;
                }
            }

            return list;
        }

        private static  OrientationState GetOrientationEnum(string orientation)
        {

            return orientation.ToUpper() switch
            {
                "N" => OrientationState.North,
                "E" => OrientationState.East,
                "S" => OrientationState.South,
                "W" => OrientationState.West,
                _ => 0,
            };
        }
    }
}
