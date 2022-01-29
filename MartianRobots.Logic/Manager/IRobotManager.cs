using MartianRobots.Models;

namespace MartianRobots.Logic.Manager
{
    public interface IRobotManager
    {
        void AssignGridAndRobots(Grid grid, IEnumerable<Robot> robots, IEnumerable<RobotCommands> robotCommands, string runName);
        Task ExecuteTasksAsync();
    }
}
