using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IRobotStepReadRepository
    {
        IEnumerable<RobotStep> GetByRunId(Guid guid);
    }
}
