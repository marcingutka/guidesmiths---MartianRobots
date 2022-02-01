namespace MartianRobots.Api.Services
{
    public interface IDeleteService
    {
        Task DeleteRunAsync(Guid runId);
    }
}
