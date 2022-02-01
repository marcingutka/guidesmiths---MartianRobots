using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class InputDataReadRepository : IInputDataReadRepository
    {
        private readonly IMongoCollection<InputData> martianRepository;

        public InputDataReadRepository(IDatabaseProvider<InputData> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public InputData GetInputByRunId(Guid runId)
        {
            return martianRepository.AsQueryable().FirstOrDefault(x => x.RunId == runId);
        }
    }
}
