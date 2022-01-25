using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.ConsoleIO.Mappers
{
    public class InputMapper : IInputMapper
    {
        public Grid MapGrid(List<string> data)
        {
            var lineSplit = data[0].Split(" ");

            return new Grid(int.Parse(lineSplit[0]), int.Parse(lineSplit[1]));
        }

        public List<Robot> MapRobots(List<string> data)
        {
            data.Remove(data[0]);

            var robotList = new List<Robot>();

            for (int i = 0; i < data.Count; i += 2)
            {
                var lineSplit = data[i].Split(" ");

                var robot = new Robot()
                {
                    Id = i / 2 + 1,
                    Position = new GridPosition()
                };

                robot.Position.X = int.Parse(lineSplit[0]);
                robot.Position.Y = int.Parse(lineSplit[1]);
                robot.Position.Orientation = GetOrientationEnum(lineSplit[2]);

                var commands = new List<char>();

                foreach (var command in data[i+1])
                {
                    commands.Add(command);
                }

                robot.Commands = GetMoveEnums(commands);

                robotList.Add(robot);
            }

            return robotList;
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
