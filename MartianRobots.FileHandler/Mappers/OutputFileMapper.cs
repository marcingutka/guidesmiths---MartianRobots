using MartianRobots.Models;
using MartianRobots.Data.Entities;

namespace MartianRobots.FileHandler.Mappers
{
    public class OutputFileMapper : IOutputFileMapper
    {
        public List<string> GenerateResults(List<Robot> robots)
        {
            var output = new List<string>();

            foreach (var robot in robots)
            {
                output.Add($"{robot}");
            }

            return output;
        }

        public List<string> GenerateResults(List<RobotStep> robots)
        {
            var modelRobots = robots.Select(x => new Robot
            {
                Position = new GridPosition { X = x.Position.X, Y = x.Position.Y, Orientation = x.Orientation },
                IsLost = x.IsLost
            }).ToList();

            return GenerateResults(modelRobots);
        }
    }
}
