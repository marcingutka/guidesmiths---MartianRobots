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
            return robotStepReadRepository.GetLostRobotsByRunId(runId).Select(lostRobotMapper.Map);
        }

        public AreaAnalitics GetAreaCalculations(Guid runId, SavedGrid gridArea)
        {
            var totalArea = (gridArea.X + 1) * (gridArea.Y + 1);
            var discoveredArea = robotStepReadRepository.GetRobotsDistinctPositions(runId).Count();

            double areaPercent = Math.Round(discoveredArea / (double)totalArea, 2) * 100;

            return new AreaAnalitics(discoveredArea, areaPercent, totalArea);
        }

        public IEnumerable<GridPoint> GetGridPoints(Guid runId)
        {
            return robotStepReadRepository.GetNumberOfRobotsForGridPoint(runId).Select(gridPointMapper.Map);
        }
    }
}
