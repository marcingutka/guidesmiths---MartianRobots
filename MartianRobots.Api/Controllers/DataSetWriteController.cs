using System.IO;
using Microsoft.AspNetCore.Mvc;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Api.Controllers
{
    [Route("api/dataSet")]
    [ApiController]
    public class DataSetWriteController : ControllerBase
    {
        private readonly IDataSetReadRepository dataSetReadRepository;
        private readonly ISavedGridReadRepository gridReadRepository;
        private readonly IMapper<DataSet, DataSetDto> mapper;

        public DataSetWriteController(
            IDataSetReadRepository dataSetReadRepository,
            ISavedGridReadRepository gridReadRepository,
            IMapper<DataSet, DataSetDto> mapper
            )
        {
            this.dataSetReadRepository = dataSetReadRepository;
            this.gridReadRepository = gridReadRepository;
            this.mapper = mapper;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile file)
        {
            var a = Request.ContentType;

            var ds = Request;
            if (file == null) return BadRequest("File is required");

            var fileName = file.FileName;

            var extension = Path.GetExtension(fileName);

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Results");
            var fullPath = Path.Combine(directoryPath, fileName);

            Directory.CreateDirectory(directoryPath);

            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            return Ok();
        }
    }
}
