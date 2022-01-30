using Microsoft.AspNetCore.Mvc;
using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Analitics.Services;
using MartianRobots.Api.Dto.AnaliticsResult;
using MartianRobots.Data.Repositories;
using MartianRobots.Models;

namespace MartianRobots.Api.Controllers
{
    [Route("api/analitics/grid/{runId}")]
    [ApiController]
    public class GridAnaliticsController : ControllerBase
    {
        private readonly IGridAnaliticsService gridAnaliticsService;
        private readonly ISavedGridReadRepository gridReadService;

        public GridAnaliticsController(
            IGridAnaliticsService gridAnaliticsService,
            ISavedGridReadRepository gridReadService
            )
        {
            this.gridAnaliticsService = gridAnaliticsService;
            this.gridReadService = gridReadService;
        }

        [HttpGet]
        public ActionResult<GridAnaliticsDto> GetGridAnaliticsByRunId(Guid runId)
        {
            var lostRobots = gridAnaliticsService.GetAllLostRobotsByRunId(runId).ToList();
            var discoveredArea = gridAnaliticsService.GetAreaCalculations(runId);
            var noOfRobotsInGridPoints = gridAnaliticsService.GetGridPoints(runId).ToList();
            var gridSize = gridReadService.GetGridByRunId(runId);

            return Ok(CreateAnaliticsData(lostRobots, discoveredArea, noOfRobotsInGridPoints, new Position { X = gridSize.X, Y = gridSize.Y }));
        }

        private static GridAnaliticsDto CreateAnaliticsData(List<LostRobot> lostRobots, AreaAnalitics area, List<GridPoint> gridPoints, Position gridSize)
        {
            return new GridAnaliticsDto(lostRobots, area, gridPoints, gridSize);
        }
    }
}
