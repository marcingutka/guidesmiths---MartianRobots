using Microsoft.AspNetCore.Mvc;
using System.Text;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Api.Services;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Api.Controllers
{
    [Route("api/robots/{runId}")]
    [ApiController]
    public class RobotsReadController : ControllerBase
    {
        private readonly IRobotStepReadRepository robotsReadRepository;
        private readonly IMapper<RobotStep, RobotStepDto> mapper;
        private readonly IDownloadResults downloadService;
        public RobotsReadController(
            IRobotStepReadRepository robotsReadRepository,
            IMapper<RobotStep, RobotStepDto> mapper,
            IDownloadResults downloadService
            )
        {
            this.robotsReadRepository = robotsReadRepository;
            this.mapper = mapper;
            this.downloadService = downloadService;
        }

        [HttpGet()]
        public ActionResult<long> GetRobotsByRunId(Guid runId)
        {
            var robotsCount = robotsReadRepository.GetRobotCountByRunId(runId);

            return Ok(robotsCount);
        }

        [HttpGet("{robotId}")]
        public ActionResult<IEnumerable<RobotStep>> GetRobotSteps(Guid runId, int robotId)
        {
            return Ok(robotsReadRepository.GetRobotSteps(runId, robotId).Select(mapper.Map));
        }

        [HttpGet("download")]
        public ActionResult Download(Guid runId)
        {
            var content = downloadService.GetResults(runId);

            var stream = new MemoryStream(content);

            var fileStream = new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = "testFDile.txt"
            };



            return fileStream;
        }
    }
}
