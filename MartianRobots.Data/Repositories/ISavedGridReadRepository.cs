using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface ISavedGridReadRepository
    {
        SavedGrid GetGridByRunId(Guid runId);
    }
}
