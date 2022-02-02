namespace MartianRobots.Api.Services
{
    public interface IUploadFileRunner
    {
        Task RunFile(IFormFile file, string runName);
    }
}
