using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;

namespace MartianRobots.Logic.Manager
{
    public interface IRobotManager
    {
        public void UploadGrid(Grid grid);
        public void ExecuteCommands(Robot robot);
    }
}
