using MartianRobots.Models;

namespace MartianRobots.Logic.Manager
{
    public interface IRobotManager
    {
        void AssignGridAndRobots(Grid grid, List<Robot> robots, List<RobotCommands> robotCommands);
        Task<Guid> ExecuteTasksAsync();
    }
}
