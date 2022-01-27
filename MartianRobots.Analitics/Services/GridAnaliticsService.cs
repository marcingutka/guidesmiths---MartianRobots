using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Analitics.Mappers;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;
using MartianRobots.Models;

namespace MartianRobots.Analitics.Services
{
    public class GridAnaliticsService : IGridAnaliticsService
    {
        private readonly IRobotStepReadRepository robotStepReadRepository;
        private readonly IMapper<RobotStep, LostRobot> lostRobotMapper;
        private readonly IMapper<Tuple<Position, int>, GridPoint> gridPointMapper;

        public GridAnaliticsService(
            IRobotStepReadRepository robotStepReadRepository,
            IMapper<RobotStep, LostRobot> lostRobotMapper,
            IMapper<Tuple<Position, int>, GridPoint> gridPointMapper
            )
        {
            this.robotStepReadRepository = robotStepReadRepository;
            this.lostRobotMapper = lostRobotMapper;
            this.gridPointMapper = gridPointMapper;
        }

        public IEnumerable<LostRobot> GetAllLostRobotsByRunId(Guid runId)
        {
            var lostRobots = robotStepReadRepository.GetLostRobotsByRunId(runId).Select(lostRobotMapper.Map);

            return lostRobots;
        }

        public AreaAnalitics GetAreaCalculations(Guid runId)
        {
            var robotSteps = robotStepReadRepository.GetRobotsDistinctPositions(runId).ToList();

            return new AreaAnalitics(robotSteps, robotSteps.Count);
        }

        public IEnumerable<GridPoint> GetGridPoints(Guid runId)
        {
            var robotsForGridPoint = robotStepReadRepository.GetNumberOfRobotsForGrid(runId);

            return robotsForGridPoint.Select(gridPointMapper.Map);
        }
    }
}
