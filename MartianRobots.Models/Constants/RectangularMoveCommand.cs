namespace MartianRobots.Models.Constants
{
    public enum RectangularMoveCommand
    {
        Left = 1,
        Right = 2,
        Forward = 3
    }

    public static class RectangularMoveCommandExtensions
    {
        public static string ToShortString(this RectangularMoveCommand command)
        {
            return command switch
            {
                RectangularMoveCommand.Left => "L",
                RectangularMoveCommand.Right => "R",
                RectangularMoveCommand.Forward => "F",
                _ => string.Empty,
            };
        }
    }
}
