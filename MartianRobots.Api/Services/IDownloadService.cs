namespace MartianRobots.Api.Services
{
    public interface IDownloadService
    {
        byte[] GetResults(Guid runId);
        byte[] GetInput(Guid runId);
    }
}
