using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MartianRobots.Api.Services;

namespace MartianRobots.Api.Controllers
{
    [Route("api/dataSet")]
    [ApiController]
    public class DataSetWriteController : ControllerBase
    {
        private readonly IUploadFileRunner fileRunner;
        private readonly IDeleteService deleteService;

        public DataSetWriteController(
            IUploadFileRunner fileRunner,
            IDeleteService deleteService
            )
        {
            this.fileRunner = fileRunner;
            this.deleteService = deleteService;
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

            try
            {
                await fileRunner.RunFile(fullPath, string.IsNullOrEmpty(name) ? fileName : name);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{runId}")]
        public async Task<ActionResult> DeleteDataSet(Guid runId)
        {
            await deleteService.DeleteRunAsync(runId);

            return Ok();
        }
    }
}
