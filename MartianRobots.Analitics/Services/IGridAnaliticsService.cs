using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Data.Entities;

namespace MartianRobots.Analitics.Services
{
    public interface IGridAnaliticsService
    {
        IEnumerable<LostRobot> GetAllLostRobotsByRunId(Guid runId);
        AreaAnalitics GetAreaCalculations(Guid runId, SavedGrid gridArea);
        IEnumerable<GridPoint> GetGridPoints(Guid runId);
    }
}
