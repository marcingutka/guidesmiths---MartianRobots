using MartianRobots.Models.Constants;

namespace MartianRobots.Models
{
    public class RobotCommands
    {
        public int Id { get; set; }
        public List<RectangularMoveCommand> Commands { get; set; }
    }
}
