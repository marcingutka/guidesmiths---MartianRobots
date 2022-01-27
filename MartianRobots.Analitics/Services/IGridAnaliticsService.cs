using MartianRobots.Analitics.AnaliticsModel;

namespace MartianRobots.Analitics.Services
{
    public interface IGridAnaliticsService
    {
        IEnumerable<LostRobot> GetAllLostRobotsByRunId(Guid runId);
        AreaAnalitics GetAreaCalculations(Guid runId);
    }
}
