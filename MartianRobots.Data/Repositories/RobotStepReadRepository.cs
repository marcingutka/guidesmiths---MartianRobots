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

        public long GetRobotsByRunId(Guid guid)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == guid && x.StepNumber == 1).Count();
        }

        public IEnumerable<RobotStep> GetRobotSteps(Guid guid, int robotId)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == guid && x.RobotId == robotId);
        }
    }
}
