using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IRobotStepWriteRepository
    {
        Task SaveRobotStepAsync(RobotStep step);
    }
}
