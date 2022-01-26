using MartianRobots.Models;

namespace MartianRobots.Logic.Manager
{
    public interface IRobotManager
    {
        void AssignRobots(Grid grid, List<Robot> robots, List<RobotCommands> robotCommands);
        void ExecuteTasks();
    }
}
