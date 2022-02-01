using System.Text;
using MartianRobots.Data.Repositories;
using MartianRobots.FileHandler.Mappers;

namespace MartianRobots.Api.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IRobotStepReadRepository robotReadRepository;
        private readonly ISavedGridReadRepository gridReadRepository;
        private readonly IOutputMapper outputMapper;

        public DownloadService(
            IRobotStepReadRepository robotReadRepository,
            ISavedGridReadRepository gridReadRepository,
            IOutputMapper outputMapper
            )
        {
            this.robotReadRepository = robotReadRepository;
            this.outputMapper = outputMapper;
            this.gridReadRepository = gridReadRepository;
        }

        public byte[] GetResults(Guid runId)
        {
            var robots = robotReadRepository.GetRobotResults(runId).ToList();

            var content = outputMapper.GenerateOutput(robots);

            var bytes = Encoding.ASCII.GetBytes(BuildOutputString(content));

            return bytes;
        }

        public byte[] GetInput(Guid runId)
        {
            var grid = gridReadRepository.GetGridByRunId(runId);

            return new byte[0];
        }

        private string BuildOutputString(List<string> content)
        {
            var sb = new StringBuilder();

            foreach (var line in content)
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
