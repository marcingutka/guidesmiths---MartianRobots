using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.Logic.Services
{
    public interface IDataTracker
    {
        Task SaveGridAsync(Grid grid);
        Task SaveRobotDataAsync();
        Task SaveRunNameAsync(string name, DateTime date);
        void CollectMetricData(int robotId, int stepNo, GridPosition position, RectangularMoveCommand? command = null, bool isLost = false);
    }
}
