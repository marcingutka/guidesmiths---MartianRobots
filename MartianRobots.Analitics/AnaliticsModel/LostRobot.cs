using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.Analitics.AnaliticsModel
{
    public record LostRobot(int RobotId, Position Position, OrientationState Orientation);
}
