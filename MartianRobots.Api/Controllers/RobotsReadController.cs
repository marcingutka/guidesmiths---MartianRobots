using Microsoft.AspNetCore.Mvc;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Api.Controllers
{
    [Route("api/robots")]
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

        [HttpGet("{runId}")]
        public ActionResult<long> GetRobotsByRunId(Guid runId)
        {
            var robotsCount = robotsReadRepository.GetRobotsByRunId(runId);

            return Ok(robotsCount);
        }

        [HttpGet("{runId}/{robotId}")]
        public ActionResult<List<RobotStep>> GetRobotSteps(Guid runId, int robotId)
        {
            return Ok(robotsReadRepository.GetRobotSteps(runId, robotId).Select(mapper.Map));
        }
    }
}
