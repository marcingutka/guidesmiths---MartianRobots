using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.FileHandler.Validator
{
    public class InputValidator : IInputValidator
    {
        public bool Validate(List<string> content)
        {
            if (content.Count < 3) throw new ValidationException("Input data is incomplete");

            var gridRgx = new Regex(@"^([1-9]|[1-4]\d|[5][0])( )([1-9]|[1-4]\d|[5][0])$");
            var robotPositionRgx = new Regex(@"^([0-9]|[1-4]\d|[5][0])( )([0-9]|[1-4]\d|[5][0])( )([N]|[E]|[S]|[W])$");
            var robotCommandRgx = new Regex(@"^(" + GenerateRegexForCommands() +@"){1,100}$");

            if (!gridRgx.IsMatch(content[0])) throw new ValidationException("Input grid data is in incorrect format");

            for (int i = 1; i < content.Count; i += 2)
            {
                if (!robotPositionRgx.IsMatch(content[i])) throw new ValidationException($"Input robot position is in incorrect for robot No. {(i + 1) / 2}");
                if (!robotCommandRgx.IsMatch(content[i + 1])) throw new ValidationException($"Input robot commands is in incorrect for robot No. {(i + 1) / 2}");
            }

            return true;
        }

        public bool CheckIfEachRobotStartsOnGrid(List<Robot> robots, Grid grid)
        {
            foreach (var robot in robots)
            {
                if (robot.Position.X > grid.X || robot.Position.Y > grid.Y) throw new ValidationException($"Initial position of robot No. {robot.Id} is outside the grid.");
            }

            return true;
        }

        private string GenerateRegexForCommands()
        {
            var commands = Enum.GetValues(typeof(RectangularMoveCommand)).Cast<RectangularMoveCommand>();

            var sb = new StringBuilder();

            foreach (var command in commands)
            {
                sb.Append($"[{command.ToShortString()}]|");
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}
