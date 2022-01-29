using Microsoft.AspNetCore.Mvc;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
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

        public RobotsReadController(
            IRobotStepReadRepository robotsReadRepository,
            IMapper<RobotStep, RobotStepDto> mapper
            )
        {
            this.robotsReadRepository = robotsReadRepository;
            this.mapper = mapper;
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
            var list = new List<string> { "TEST FILE" };
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(ms);
                tw.Write(list[0]);
                tw.Flush();
                ms.Position = 0;
                bytes = ms.ToArray();
            }

            return Ok(bytes);
        }
    }
}
