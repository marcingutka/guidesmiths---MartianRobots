using MongoDB.Driver;

namespace MartianRobots.Data.Providers
{
    public interface IDatabaseProvider<T>
    {
        IMongoCollection<T> GetCollection();
    }
}
