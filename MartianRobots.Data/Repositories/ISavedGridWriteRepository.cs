using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface ISavedGridWriteRepository
    {
        bool CheckRunId(Guid runId);
        Task SaveGridAsync(SavedGrid grid);
        Task DeleteRunAsync(Guid runId);
    }
}
