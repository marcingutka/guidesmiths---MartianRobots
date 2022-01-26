using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface ISavedGridWriteRepository
    {
        Task SaveGridAsync(SavedGrid grid);
    }
}
