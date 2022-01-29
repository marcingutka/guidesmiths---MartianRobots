using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class SavedGridWriteRepository : ISavedGridWriteRepository
    {
        private readonly IMongoCollection<SavedGrid> martianRepository;

        public SavedGridWriteRepository(IDatabaseProvider<SavedGrid> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public bool CheckRunId(Guid runId)
        {
            return !martianRepository.AsQueryable().Any(x => x.RunId == runId);
        }

        public async Task SaveGridAsync(SavedGrid grid)
        {
            await martianRepository.InsertOneAsync(grid);
        }
    }
}
