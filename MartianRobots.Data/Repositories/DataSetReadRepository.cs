using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class DataSetReadRepository : IDataSetReadRepository
    {
        private readonly IMongoCollection<DataSet> martianRepository;

        public DataSetReadRepository(IDatabaseProvider<DataSet> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public IEnumerable<DataSet> GetAllSets()
        {
            return martianRepository.AsQueryable().OrderByDescending(s => s.GenerationDate);
        }
    }
}
