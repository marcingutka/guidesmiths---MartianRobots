using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IInputDataWriteRepository
    {
        Task SaveInputAsync(InputData data);
        Task DeleteInputAsync(Guid runId);
    }
}
