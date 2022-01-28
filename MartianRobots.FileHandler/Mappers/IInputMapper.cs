using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;

namespace MartianRobots.FileHandler.Mappers
{
    public interface IInputMapper
    {
        (Grid Grid, IEnumerable<Robot> Robots, IEnumerable<RobotCommands> Commands) Map(List<string> data);
    }
}
