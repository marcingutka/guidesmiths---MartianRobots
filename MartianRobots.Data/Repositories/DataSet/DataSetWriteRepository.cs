using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;
using MongoDB.Driver;

namespace MartianRobots.Data.Repositories
{
    public class DataSetWriteRepository : IDataSetWriteRepository
    {
        private readonly IMongoCollection<DataSet> martianRepository;

        public DataSetWriteRepository(IDatabaseProvider<DataSet> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public async Task SaveDataSetAsync(DataSet name)
        {
            await martianRepository.InsertOneAsync(name);
        }

        public async Task DeleteDataSetAsync(Guid runId)
        {
            await martianRepository.DeleteOneAsync(x => x.RunId == runId);
        }
    }
}
