using MartianRobots.Models;

namespace MartianRobots.FileHandler.Validator
{
    public interface IInputValidator
    {
        bool Validate(List<string> content);
        bool CheckIfEachRobotStartsOnGrid(List<Robot> robots, Grid grid);
    }
}
