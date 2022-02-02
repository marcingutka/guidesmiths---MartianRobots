using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MartianRobots.Api.Controllers
{
    [Route("api/robots/{runId}")]
    [ApiController]
    public class RobotsReadController : ControllerBase
    {
        private readonly IRobotStepReadRepository robotsReadRepository;

        public RobotsReadController(
            IRobotStepReadRepository robotsReadRepository
            )
        {
            this.robotsReadRepository = robotsReadRepository;
        }

        [HttpGet()]
        public ActionResult<long> GetRobotsByRunId(Guid runId)
        {
            var robotsCount = robotsReadRepository.GetRobotCountByRunId(runId);

            return Ok(robotsCount);
        }

        [HttpGet("robot/{robotId}")]
        public ActionResult<IEnumerable<RobotStep>> GetRobotSteps(Guid runId, int robotId)
        {
            return Ok(robotsReadRepository.GetRobotSteps(runId, robotId));
        }
    }
}
