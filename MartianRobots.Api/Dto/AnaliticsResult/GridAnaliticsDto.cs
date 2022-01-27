using MartianRobots.Analitics.AnaliticsModel;

namespace MartianRobots.Api.Dto.AnaliticsResult
{
    public record GridAnaliticsDto(List<LostRobot> LostRobots, AreaAnalitics DiscoveredArea, List<GridPoint> GridPoints);
}
