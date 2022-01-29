using MartianRobots.Models;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IOutputMapper
    {
        List<string> GenerateOutput(Grid grid, List<Robot> robots);
    }
}
