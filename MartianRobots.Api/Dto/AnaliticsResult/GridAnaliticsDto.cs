using MartianRobots.Analitics.AnaliticsModel;

namespace MartianRobots.Api.Dto.AnaliticsResult
{
    public class GridAnaliticsDto
    {
        public List<LostRobot> LostRobots { get; set;}

        public AreaAnalitics DiscoveredArea { get; set;}
    }
}
