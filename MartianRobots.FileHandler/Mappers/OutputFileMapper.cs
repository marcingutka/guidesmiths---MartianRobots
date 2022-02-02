using MartianRobots.Data.Entities;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using System.Text;

namespace MartianRobots.FileHandler.Mappers
{
    public class OutputFileMapper : IOutputFileMapper
    {

        public List<string> GenerateResults(List<RobotStep> robots)
        {
            var modelRobots = robots.Select(x => new Robot
            {
                Position = new GridPosition { X = x.Position.X, Y = x.Position.Y, Orientation = x.Orientation },
                IsLost = x.IsLost
            }).ToList();

            return GenerateResults(modelRobots);
        }

        public List<string> GenerateInputFile(InputData input)
        {
            var content = new List<string>();

            content.Add($"{input.Grid.X} {input.Grid.Y}");

            for (var i = 0; i < input.Robots.Count; i++)
            {
                content.Add($"{input.Robots[i]}");
                content.Add($"{GetCommandLine(input.Commands[i].Commands)}");
            }

            return content;
        }

        private List<string> GenerateResults(List<Robot> robots)
        {
            var output = new List<string>();

            foreach (var robot in robots)
            {
                output.Add($"{robot}");
            }

            return output;
        }

        private string GetCommandLine(List<RectangularMoveCommand> commands)
        {
            var sb = new StringBuilder();

            foreach (var command in commands)
            {
                sb.Append($"{command.ToShortString()}");
            }

            return sb.ToString();
        }
    }
}
