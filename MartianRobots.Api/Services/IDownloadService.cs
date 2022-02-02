using Microsoft.AspNetCore.Mvc;

namespace MartianRobots.Api.Services
{
    public interface IDownloadService
    {
        FileStreamResult PrepareResultFile(Guid runId, string fileName);
        FileStreamResult PrepareInputFile(Guid runId, string fileName);
    }
}
