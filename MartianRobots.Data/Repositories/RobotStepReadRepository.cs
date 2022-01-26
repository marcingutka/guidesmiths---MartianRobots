using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class RobotStepReadRepository: IRobotStepReadRepository
    {
        private readonly IMongoCollection<RobotStep> martianRepository;

        public RobotStepReadRepository(IDatabaseProvider<RobotStep> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public IEnumerable<RobotStep> GetByRunId(Guid guid)
        {
            return martianRepository.Find(x => x.RunId == guid).ToEnumerable();
        }
    }
}
