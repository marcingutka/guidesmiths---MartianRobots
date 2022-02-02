using Microsoft.AspNetCore.Http;

namespace MartianRobots.FileHandler
{
    public interface IFileHandler
    {
        public List<string> ReadFile(IFormFile fileContent);

        public void WriteFile(IEnumerable<string> robots, string filePath);
    }
}
