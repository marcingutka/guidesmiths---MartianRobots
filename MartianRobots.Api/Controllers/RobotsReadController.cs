﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IDataSetReadRepository dataSetReadRepository;
        private readonly IMapper<RobotStep, RobotStepDto> mapper;
        private readonly IDownloadResults downloadService;


        public RobotsReadController(
            IRobotStepReadRepository robotsReadRepository,
            IDataSetReadRepository dataSetReadRepository,
            IMapper<RobotStep, RobotStepDto> mapper,
            IDownloadResults downloadService
            )
        {
            this.robotsReadRepository = robotsReadRepository;
            this.dataSetReadRepository = dataSetReadRepository;
            this.mapper = mapper;
            this.downloadService = downloadService;
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
