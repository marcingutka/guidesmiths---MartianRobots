using MartianRobots.Models;
using MartianRobots.Data.Entities;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IOutputMapper
    {
        List<string> GenerateOutput(List<Robot> robots);
        List<string> GenerateOutput(List<RobotStep> robots);
    }
}
