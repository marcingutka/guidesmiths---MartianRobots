using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Analitics.Mappers;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Analitics.Services
{
    public class GridAnaliticsService : IGridAnaliticsService
    {
        private readonly IRobotStepReadRepository robotStepReadRepository;
        private readonly IMapper<RobotStep, LostRobot> lostRobotMapper;

        public GridAnaliticsService(
            IRobotStepReadRepository robotStepReadRepository,
            IMapper<RobotStep, LostRobot> lostRobotMapper
            )
        {
            this.robotStepReadRepository = robotStepReadRepository;
            this.lostRobotMapper = lostRobotMapper;
        }

        public IEnumerable<LostRobot> GetAllLostRobotsByRunId(Guid runId)
        {
            var lostRobots = robotStepReadRepository.GetLostRobotsByRunId(runId).Select(lostRobotMapper.Map);

            return lostRobots;
        }

        public AreaAnalitics GetAreaCalculations(Guid runId)
        {
            var robotSteps = robotStepReadRepository.GetRobotsDistinctPositions(runId).ToList();

            return new AreaAnalitics { DiscaveredPoints = robotSteps, DiscaveredAreaAbsolute = robotSteps.Count};
        }
    }
}
