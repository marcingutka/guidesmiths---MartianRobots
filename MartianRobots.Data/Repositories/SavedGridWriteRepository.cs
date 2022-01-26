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

        public async Task SaveGridAsync(SavedGrid grid)
        {
            await martianRepository.InsertOneAsync(grid);
        }
    }
}
