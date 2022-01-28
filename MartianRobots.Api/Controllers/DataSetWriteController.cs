using System.IO;
using Microsoft.AspNetCore.Mvc;
using MartianRobots.Api.Services;

namespace MartianRobots.Api.Controllers
{
    [Route("api/dataSet")]
    [ApiController]
    public class DataSetWriteController : ControllerBase
    {
        private readonly IUploadFileRunner fileRunner;

        public DataSetWriteController(IUploadFileRunner fileRunner)
        {
            this.fileRunner = fileRunner;
        }

        [HttpPost("upload/{name}")]
        public async Task<ActionResult> RunFromUploadedFile([FromForm] IFormFile file, string name)
        {
            if (file == null) return BadRequest("File is required");

            var fileName = file.FileName;

            var extension = Path.GetExtension(fileName);
            if (extension != ".txt") return BadRequest(".txt file extension is required");

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Results");
            var fullPath = Path.Combine(directoryPath, fileName);

            Directory.CreateDirectory(directoryPath);

            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            await fileRunner.RunFile(fullPath, string.IsNullOrEmpty(name)? fileName : name);

            return Ok();
        }
    }
}
