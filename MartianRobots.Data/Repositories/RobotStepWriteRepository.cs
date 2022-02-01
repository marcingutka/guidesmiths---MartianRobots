using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;

namespace MartianRobots.Data.Repositories
{
    public class RobotStepWriteRepository : IRobotStepWriteRepository
    {
        private readonly IMongoCollection<RobotStep> martianRepository;

        public RobotStepWriteRepository(IDatabaseProvider<RobotStep> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public async Task SaveRobotStepAsync(RobotStep step)
        {
            await martianRepository.InsertOneAsync(step);
        }

        public async Task SaveRobotMovement(List<RobotStep> robotPositions)
        {
            await martianRepository.InsertManyAsync(robotPositions);
        }

        public async Task DeleteRobotStepsAsync(Guid runId)
        {
            await martianRepository.DeleteManyAsync(x => x.RunId == runId);
        }
    }
}
