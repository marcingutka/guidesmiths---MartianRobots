namespace MartianRobots.Api.Services
{
    public interface IUploadFileRunner
    {
        Task RunFile(string path, string runName);
    }
}
