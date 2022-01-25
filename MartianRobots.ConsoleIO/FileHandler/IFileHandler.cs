using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.ConsoleIO.FileHandler
{
    interface IFileHandler
    {
        public List<string> ReadFile(string filePath);

        public void WriteFile(IEnumerable<string> robots, string filePath);
    }
}
