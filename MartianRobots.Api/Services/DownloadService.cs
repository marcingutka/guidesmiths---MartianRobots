using System.Text;
using MartianRobots.Data.Repositories;
using MartianRobots.FileHandler.Mappers;

namespace MartianRobots.Api.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IRobotStepReadRepository robotReadRepository;
        private readonly IInputDataReadRepository inputReadRepository;
        private readonly IOutputFileMapper outputFileMapper;

        public DownloadService(
            IRobotStepReadRepository robotReadRepository,
            IInputDataReadRepository inputReadRepository,
            IOutputFileMapper outputMapper
            )
        {
            this.robotReadRepository = robotReadRepository;
            this.outputFileMapper = outputMapper;
            this.inputReadRepository = inputReadRepository;
        }

        public byte[] GetResults(Guid runId)
        {
            var robots = robotReadRepository.GetRobotResults(runId).ToList();

            var content = outputFileMapper.GenerateResults(robots);

            return Encoding.ASCII.GetBytes(BuildOutputString(content));
        }

        public byte[] GetInput(Guid runId)
        {
            var inputData = inputReadRepository.GetInputByRunId(runId);

            var content = outputFileMapper.GenerateInputFile(inputData);

            return Encoding.ASCII.GetBytes(BuildOutputString(content));
        }

        private static string BuildOutputString(List<string> content)
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
