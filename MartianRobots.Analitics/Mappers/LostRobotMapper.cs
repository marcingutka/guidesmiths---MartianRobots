using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Data.Entities;

namespace MartianRobots.Analitics.Mappers
{
    public class LostRobotMapper : IMapper<RobotStep, LostRobot>
    {
        public LostRobot Map(RobotStep entity)
        {
            return new LostRobot(entity.RobotId, entity.Position, entity.Orientation);
        }
    }
}
