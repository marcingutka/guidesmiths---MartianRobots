using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

        public List<Tuple<Position, int>> GetNumberOfRobotsForGrid(Guid runId)
        {
            var result = martianRepository.Aggregate().Match(x => x.RunId == runId).Group(x => x.Position, z => new Tuple<Position, IEnumerable<int>>(z.Key, z.Select(x => x.RobotId))).ToList();
            var result1 = result.Select(x => new Tuple<Position, int>(x.Item1, x.Item2.Distinct().Count())).OrderBy(x => x.Item1.X).ThenBy(x => x.Item1.Y).ToList();

            return result1;
        }

        public IEnumerable<RobotStep> GetRobotResults(Guid runId)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == runId && x.IsLastStep == true).OrderBy(x => x.RobotId);
        }

        public IEnumerable<RobotStep> GetRobotInput(Guid runId)
        {
            return martianRepository.AsQueryable().Where(x => x.RunId == runId && x.StepNumber == 1).OrderBy(x => x.RobotId);
        }
    }
}
