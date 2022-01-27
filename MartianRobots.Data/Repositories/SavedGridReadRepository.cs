using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class SavedGridReadRepository : ISavedGridReadRepository
    {
        private readonly IMongoCollection<SavedGrid> martianRepository;

        public SavedGridReadRepository(IDatabaseProvider<SavedGrid> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public SavedGrid GetGridByRunId(Guid runId)
        {
            var grid = martianRepository.Find(x => x.RunId == runId).SingleOrDefault();
            return grid;
        }
    }
}
