using MartianRobots.Models.Constants;

namespace MartianRobots.Api.Dto
{
    public class RobotStepDto
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public OrientationState Orientation { get; set; }
        public RectangularMoveCommand? Command { get; set; }
        public bool IsLost { get; set; }
    }
}
