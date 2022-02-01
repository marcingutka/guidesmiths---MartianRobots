using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class InputDataWriteRepository : IInputDataWriteRepository
    {
        private readonly IMongoCollection<InputData> martianRepository;

        public InputDataWriteRepository(IDatabaseProvider<InputData> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public async Task SaveInputAsync(InputData data)
        {
            await martianRepository.InsertOneAsync(data);
        }

        public async Task DeleteInputAsync(Guid runId)
        {
            await martianRepository.DeleteOneAsync(x => x.RunId == runId);
        }
    }
}
