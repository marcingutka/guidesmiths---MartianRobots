using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;

namespace MartianRobots.Logic.Validators
{
    public interface IPositionValidator
    {
        public bool IsRobotOffGrid(Position currentPosition, Grid Grid);

        public bool IsRobotLost(Position position, List<Position> edgePositions);
    }
}
