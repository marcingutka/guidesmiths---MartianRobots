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

        public async Task SaveInput(InputData data)
        {
            await martianRepository.InsertOneAsync(data);
        }
    }
}
