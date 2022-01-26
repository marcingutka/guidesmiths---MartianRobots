using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IDataNameWriteRepository
    {
        Task SaveNameAsync(DataName name);
    }
}
