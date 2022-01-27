using Microsoft.AspNetCore.Mvc;
using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Analitics.Services;
using MartianRobots.Api.Dto.AnaliticsResult;

namespace MartianRobots.Api.Controllers
{
    [Route("api/analitics/grid/{runId}")]
    [ApiController]
    public class GridAnaliticsController : ControllerBase
    {
        private readonly IGridAnaliticsService gridAnaliticsService;

        public GridAnaliticsController(
            IGridAnaliticsService gridAnaliticsService
            )
        {
            this.gridAnaliticsService = gridAnaliticsService;
        }

        [HttpGet]
        public ActionResult<GridAnaliticsDto> GetGridAnaliticsByRunId(Guid runId)
        {
            var lostRobots = gridAnaliticsService.GetAllLostRobotsByRunId(runId).ToList();
            var discoveredArea = gridAnaliticsService.GetAreaCalculations(runId);

            return Ok(CreateAnaliticsData(lostRobots, discoveredArea));
        }

        private static GridAnaliticsDto CreateAnaliticsData(List<LostRobot> lostRobots, AreaAnalitics area)
        {
            return new GridAnaliticsDto
            {
                LostRobots = lostRobots,
                DiscoveredArea = area,
            };
        }
    }
}
