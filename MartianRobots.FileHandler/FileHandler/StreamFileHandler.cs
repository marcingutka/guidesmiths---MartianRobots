using Microsoft.AspNetCore.Http;

namespace MartianRobots.FileHandler
{
    public class StreamFileHandler : IFileHandler
    {
        public List<string> ReadFile(IFormFile file)
        {
            var fileContent = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    fileContent.Add(reader.ReadLine());
                }
            }

            return fileContent;
        }

        public void WriteFile(IEnumerable<string> robots, string filePath)
        {
            File.WriteAllLines(filePath, robots);
        }
    }
}
