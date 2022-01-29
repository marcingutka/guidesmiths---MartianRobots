using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class DataSetWriteRepository : IDataSetWriteRepository
    {
        private readonly IMongoCollection<DataSet> martianRepository;

        public DataSetWriteRepository(IDatabaseProvider<DataSet> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public async Task SaveNameAsync(DataSet name)
        {
            await martianRepository.InsertOneAsync(name);
        }

        public async Task DeleteDataSetAsync(Guid runId)
        {
            await martianRepository.DeleteOneAsync(x => x.RunId == runId);
        }
    }
}
