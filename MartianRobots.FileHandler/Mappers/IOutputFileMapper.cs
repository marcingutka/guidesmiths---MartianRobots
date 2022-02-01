using MartianRobots.Models;
using MartianRobots.Data.Entities;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IOutputFileMapper
    {
        List<string> GenerateResults(List<Robot> robots);
        List<string> GenerateResults(List<RobotStep> robots);
    }
}
