using Microsoft.AspNetCore.Mvc;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Api.Services;
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
        private readonly IDownloadService downloadService;

        const string txtExtension = ".txt";

        public DataSetReadController(
            IDataSetReadRepository dataSetReadRepository,
            ISavedGridReadRepository gridReadRepository,
            IMapper<DataSet, DataSetDto> mapper,
            IDownloadService downloadService
            )
        {
            this.dataSetReadRepository = dataSetReadRepository;
            this.gridReadRepository = gridReadRepository;
            this.mapper = mapper;
            this.downloadService = downloadService;
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

        [HttpGet("results/{runId}/download")]
        public ActionResult DownloadResults(Guid runId)
        {
            var fileName = dataSetReadRepository.GetSetNameByRunId(runId);
            if (fileName.Length > 4 && !(fileName[^4..].ToLower() == txtExtension)) fileName += txtExtension;

            var content = downloadService.GetResults(runId);

            var stream = new MemoryStream(content);

            var fileStream = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };

            return fileStream;
        }

        [HttpGet("input/{runId}/download")]
        public ActionResult DownloadInput(Guid runId)
        {
            var fileName = dataSetReadRepository.GetSetNameByRunId(runId);
            if (fileName.Length > 4 && !(fileName[^4..].ToLower() == txtExtension)) fileName += txtExtension;

            var content = downloadService.GetInput(runId);

            var stream = new MemoryStream(content);

            var fileStream = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };

            return fileStream;
        }
    }
}
