using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;
using MongoDB.Driver;

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

        public string GetSetNameByRunId(Guid ruinId)
        {
            return martianRepository.AsQueryable().FirstOrDefault(x => x.RunId == ruinId).Name;
        }
    }
}
