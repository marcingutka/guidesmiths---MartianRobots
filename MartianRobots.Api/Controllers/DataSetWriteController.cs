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

            try
            {
                await fileRunner.RunFile(file, string.IsNullOrEmpty(name) ? fileName : name);
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
