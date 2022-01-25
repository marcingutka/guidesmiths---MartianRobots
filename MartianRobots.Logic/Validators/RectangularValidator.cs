using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;

namespace MartianRobots.Logic.Validators
{
    public record RectangularValidator : IPositionValidator
    {
        public bool IsRobotOffGrid(Position currentPosition, Grid Grid)
        {
            return currentPosition.X > Grid.X || currentPosition.X < 0 || currentPosition.Y > Grid.Y || currentPosition.Y < 0;
        }

        public bool IsRobotLost(Position position, List<Position> edgePositions)
        {
            return !edgePositions.Contains(position);
        }
    }
}
