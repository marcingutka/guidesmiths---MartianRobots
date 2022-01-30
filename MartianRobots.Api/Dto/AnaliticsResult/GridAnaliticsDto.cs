using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Models;

namespace MartianRobots.Api.Dto.AnaliticsResult
{
    public record GridAnaliticsDto(List<LostRobot> LostRobots, AreaAnalitics DiscoveredArea, List<GridPoint> GridPoints, Position gridSize);
}
