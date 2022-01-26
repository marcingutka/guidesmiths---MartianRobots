using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Logic.Mappers
{
    public interface IMapper<T1, T2>
    {
        T2 Map(Guid guid, T1 robot, string command);
    }
}
