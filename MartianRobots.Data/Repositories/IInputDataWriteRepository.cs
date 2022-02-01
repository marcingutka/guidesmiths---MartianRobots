using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IInputDataWriteRepository
    {
        Task SaveInput(InputData data);
    }
}
