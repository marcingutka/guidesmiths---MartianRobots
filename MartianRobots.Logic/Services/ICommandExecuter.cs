using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;

namespace MartianRobots.Logic.Services
{
    public interface ICommandExecuter<T>
    {
        public GridPosition Execute(GridPosition position, T action);
    }
}
