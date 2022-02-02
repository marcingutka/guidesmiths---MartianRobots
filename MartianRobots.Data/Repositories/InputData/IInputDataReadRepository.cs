using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IInputDataReadRepository
    {
        InputData GetInputByRunId(Guid runId);
    }
}
