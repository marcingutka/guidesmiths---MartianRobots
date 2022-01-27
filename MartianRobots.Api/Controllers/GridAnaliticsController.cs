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
            var noOfRobotsInGridPoints = gridAnaliticsService.GetGridPoints(runId).ToList();

            return Ok(CreateAnaliticsData(lostRobots, discoveredArea, noOfRobotsInGridPoints));
        }

        private static GridAnaliticsDto CreateAnaliticsData(List<LostRobot> lostRobots, AreaAnalitics area, List<GridPoint> gridPoints)
        {
            return new GridAnaliticsDto(lostRobots, area, gridPoints);
        }
    }
}
