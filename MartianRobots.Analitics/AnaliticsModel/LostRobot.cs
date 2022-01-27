using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.Analitics.AnaliticsModel
{
    public class LostRobot
    {
        public int RobotId { get; set; }
        public Position Position { get; set; }
        public OrientationState Orientation { get; set; }

    }
}
