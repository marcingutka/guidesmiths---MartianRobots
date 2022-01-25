using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.ConsoleIO.FileHandler
{
    public class TxtFileHandler : IFileHandler
    {
        public List<string> ReadFile(string filePath)
        {
            return File.ReadAllLines(filePath).ToList();
        }

        public void WriteFile(IEnumerable<string> robots, string filePath)
        {
            File.WriteAllLines(filePath, robots);
        }
    }
}
