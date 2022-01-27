using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IDataSetWriteRepository
    {
        Task SaveNameAsync(DataSet name);
    }
}
