using MartianRobots.Models;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IInputMapper
    {
        (Grid Grid, IEnumerable<Robot> Robots, IEnumerable<RobotCommands> Commands) Map(List<string> data);
    }
}
