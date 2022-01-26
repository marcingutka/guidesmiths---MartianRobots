using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class DataNameWriteRepository : IDataNameWriteRepository
    {
        private readonly IMongoCollection<DataName> martianRepository;

        public DataNameWriteRepository(IDatabaseProvider<DataName> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public async Task SaveNameAsync(DataName name)
        {
            await martianRepository.InsertOneAsync(name);
        }
    }
}
