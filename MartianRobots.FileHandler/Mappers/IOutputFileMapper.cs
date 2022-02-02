using MartianRobots.Data.Entities;
using MartianRobots.Models;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IOutputFileMapper
    {
        List<string> GenerateResults(List<RobotStep> robots);
        List<string> GenerateInputFile(InputData input);
    }
}
