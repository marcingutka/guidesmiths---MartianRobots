using MartianRobots.Models;

namespace MartianRobots.Logic.Services
{
    public interface ICommandExecuter<T>
    {
        public GridPosition Execute(GridPosition position, T action);
    }
}
