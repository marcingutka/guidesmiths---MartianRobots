using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IRobotStepWriteRepository
    {
        Task SaveRobotStepAsync(RobotStep step);
        Task SaveRobotMovement(List<RobotStep> robotPositions);
        Task DeleteRobotStepsAsync(Guid runId);
    }
}
