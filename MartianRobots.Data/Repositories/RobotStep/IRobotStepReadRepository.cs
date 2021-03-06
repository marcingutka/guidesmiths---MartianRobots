using MartianRobots.Data.Entities;
using MartianRobots.Models;

namespace MartianRobots.Data.Repositories
{
    public interface IRobotStepReadRepository
    {
        long GetRobotCountByRunId(Guid runId);
        IEnumerable<RobotStep> GetRobotSteps(Guid runId, int robotId);
        IEnumerable<RobotStep> GetLostRobotsByRunId(Guid runId);
        IEnumerable<Position> GetRobotsDistinctPositions(Guid runId);
        IEnumerable<Tuple<Position, int>> GetNumberOfRobotsForGridPoint(Guid runId);
        IEnumerable<RobotStep> GetRobotResults(Guid runId);
        IEnumerable<RobotStep> GetRobotInput(Guid runId);
    }
}
