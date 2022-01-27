using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Data.Entities;

namespace MartianRobots.Analitics.Mappers
{
    public class LostRobotMapper : IMapper<RobotStep, LostRobot>
    {
        public LostRobot Map(RobotStep entity)
        {
            return new LostRobot
            {
                RobotId = entity.RobotId,
                Position = entity.Position,
                Orientation = entity.Orientation,
            };
        }
    }
}
