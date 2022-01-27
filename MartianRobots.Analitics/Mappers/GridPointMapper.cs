using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Models;

namespace MartianRobots.Analitics.Mappers
{
    public class GridPointMapper : IMapper<Tuple<Position, int>, GridPoint>
    {
        public GridPoint Map(Tuple<Position, int> entity)
        {
            return new GridPoint(entity.Item1, entity.Item2);
        }
    }
}
