using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IRobotStepReadRepository
    {
        long GetRobotsByRunId(Guid guid);
        IEnumerable<RobotStep> GetRobotSteps(Guid guid, int robotId);
    }
}
