using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;

namespace MartianRobots.ConsoleIO.Mappers
{
    interface IInputMapper
    {
        public Grid MapGrid(List<string> data);

        public List<Robot> MapRobots(List<string> data);
    }
}
