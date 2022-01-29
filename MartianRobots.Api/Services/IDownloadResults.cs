namespace MartianRobots.Api.Services
{
    public interface IDownloadResults
    {
        byte[] GetResults(Guid runId);
    }
}
