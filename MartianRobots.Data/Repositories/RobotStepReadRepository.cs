using System.Linq;
using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;
using MartianRobots.Models;

namespace MartianRobots.Data.Repositories
{
    public class RobotStepReadRepository: IRobotStepReadRepository
    {
        private readonly IMongoCollection<RobotStep> martianRepository;

        public RobotStepReadRepository(IDatabaseProvider<RobotStep> provider)
        {
            martianRepository = provider.GetCollection();
        }

        public long GetRobotCountByRunId(Guid runId)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == runId && x.StepNumber == 1).Count();
        }

        public IEnumerable<RobotStep> GetRobotSteps(Guid runId, int robotId)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == runId && x.RobotId == robotId);
        }

        public IEnumerable<RobotStep> GetLostRobotsByRunId(Guid runId)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == runId && x.IsLost == true);
        }

        public IEnumerable<Position> GetRobotsDistinctPositions(Guid runId)
        {
            var result = martianRepository.AsQueryable().Select(x => x.Position).Distinct().OrderBy(x => x.X).ThenBy(x => x.Y);
            return result;
        }
    }
}
