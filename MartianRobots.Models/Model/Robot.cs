using MartianRobots.Models.Constants;

namespace MartianRobots.Models
{
    public class Robot
    {
        public int Id { get; set; }
        public GridPosition Position { get; set; }
        public bool IsLost { get; set; } = false;

        public override string ToString()
        {
            return $"{Position.X} {Position.Y} {Position.Orientation.ToShortString()} {(IsLost == true ? "LOST" : string.Empty)}";
        }
    }
}
