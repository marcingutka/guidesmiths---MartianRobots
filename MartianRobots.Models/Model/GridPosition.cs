using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models.Constants;

namespace MartianRobots.Models
{
    public class GridPosition : Position
    {
        public OrientationState Orientation { get; set; }
    }
}
