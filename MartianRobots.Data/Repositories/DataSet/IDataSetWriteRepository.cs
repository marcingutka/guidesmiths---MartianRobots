using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IDataSetWriteRepository
    {
        Task SaveDataSetAsync(DataSet name);
        Task DeleteDataSetAsync(Guid runId);
    }
}
