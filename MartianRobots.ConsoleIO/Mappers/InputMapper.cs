﻿using System;
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
        public (Grid Grid, IEnumerable<Robot> Robots, IEnumerable<RobotCommands> Commands) Map(List<string> data)
        {
            var grid = MapGrid(data[0]);

            var robots = MapRobots(data.Skip(1).ToList());

            return (grid, robots.Item1, robots.Item2);
        }

        private Grid MapGrid(string data)
        {
            var lineSplit = data.Split(" ");

            return new Grid(int.Parse(lineSplit[0]), int.Parse(lineSplit[1]));
        }

        private (IEnumerable<Robot>, IEnumerable<RobotCommands>) MapRobots(List<string> data)
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

        private Robot MapRobot(string robotPosition, int enumerator)
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

        private List<char> MapCommands(string commands)
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
