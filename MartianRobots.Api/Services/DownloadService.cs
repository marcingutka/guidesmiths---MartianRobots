using System.Text;
using MartianRobots.Data.Repositories;
using MartianRobots.FileHandler.Mappers;

namespace MartianRobots.Api.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IRobotStepReadRepository robotReadRepository;
        private readonly IInputDataReadRepository inputReadRepository;
        private readonly IOutputFileMapper outputMapper;

        public DownloadService(
            IRobotStepReadRepository robotReadRepository,
            IInputDataReadRepository inputReadRepository,
            IOutputFileMapper outputMapper
            )
        {
            this.robotReadRepository = robotReadRepository;
            this.outputMapper = outputMapper;
            this.inputReadRepository = inputReadRepository;
        }

        public byte[] GetResults(Guid runId)
        {
            var robots = robotReadRepository.GetRobotResults(runId).ToList();

            var content = outputMapper.GenerateResults(robots);

            var bytes = Encoding.ASCII.GetBytes(BuildOutputString(content));

            return bytes;
        }

        public byte[] GetInput(Guid runId)
        {
            var inputData = inputReadRepository.GetInputByRunId(runId);

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
