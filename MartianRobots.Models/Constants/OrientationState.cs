
namespace MartianRobots.Models.Constants
{
    public enum OrientationState
    {
        North = 1,
        East = 2,
        South = 3,
        West = 4,
    }

    public static class OrientationStateExtensions
    {
        public static string ToShortString(this OrientationState state)
        {
            return state switch
            {
                OrientationState.North => "N",
                OrientationState.East => "E",
                OrientationState.South => "S",
                OrientationState.West => "W",
                _ => string.Empty,
            };
        }
    }
}
