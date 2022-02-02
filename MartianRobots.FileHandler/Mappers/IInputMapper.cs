using MartianRobots.Models;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IInputMapper
    {
        (Grid Grid, List<Robot> Robots, List<RobotCommands> Commands) Map(List<string> data);
    }
}
