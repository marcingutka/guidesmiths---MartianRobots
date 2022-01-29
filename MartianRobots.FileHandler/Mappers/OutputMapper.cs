using MartianRobots.Models;

namespace MartianRobots.FileHandler.Mappers
{
    public class OutputMapper : IOutputMapper
    {
        public List<string> GenerateOutput(Grid grid, List<Robot> robots)
        {
            var output = new List<string>();

            output.Add($"{grid.X} {grid.Y}");
            foreach (var robot in robots)
            {
                output.Add($"{robot}");
            }

            return output;
        }
    }
}
