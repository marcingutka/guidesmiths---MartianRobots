using System.Text;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;
using MartianRobots.FileHandler.Mappers;

namespace MartianRobots.Api.Services
{
    public class DownloadResults : IDownloadResults
    {
        private readonly IRobotStepReadRepository robotReadRepository;
        private readonly IOutputMapper outputMapper;

        public DownloadResults(
            IRobotStepReadRepository robotReadRepository,
            IOutputMapper outputMapper
            )
        {
            this.robotReadRepository = robotReadRepository;
            this.outputMapper = outputMapper;
        }

        public byte[] GetResults(Guid runId)
        {
            var robots = robotReadRepository.GetRobotResults(runId).ToList();

            var bytes = Encoding.ASCII.GetBytes(BuildOutputString(robots));

            return bytes;
        }

        private string BuildOutputString(List<RobotStep> robots)
        {
            var results = outputMapper.GenerateOutput(robots);

            var sb = new StringBuilder();

            foreach (var result in results)
            {
                sb.AppendLine(result);
            }

            return sb.ToString();
        }
    }
}
