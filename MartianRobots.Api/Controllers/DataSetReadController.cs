using Microsoft.AspNetCore.Mvc;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Api.Controllers
{
    [Route("api/dataSet")]
    [ApiController]
    public class DataSetReadController : ControllerBase
    {
        private readonly IDataSetReadRepository dataSetReadRepository;
        private readonly ISavedGridReadRepository gridReadRepository;
        private readonly IMapper<DataSet, DataSetDto> mapper;

        public DataSetReadController(
            IDataSetReadRepository dataSetReadRepository,
            ISavedGridReadRepository gridReadRepository,
            IMapper<DataSet, DataSetDto> mapper
            )
        {
            this.dataSetReadRepository = dataSetReadRepository;
            this.gridReadRepository = gridReadRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataSetDto>> GetAllDataSets()
        {
            var dataSets = dataSetReadRepository.GetAllSets().Select(mapper.Map);

            return Ok(dataSets);
        }

        [HttpGet("grid/{runId}")]
        public ActionResult<GridDto> GetGridSize(Guid runId)
        {
            var grid = gridReadRepository.GetGridByRunId(runId);

            return Ok(new GridDto { X = grid.X, Y = grid.Y});
        }
    }
}
