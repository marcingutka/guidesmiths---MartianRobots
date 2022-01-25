using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots.Models;
using MartianRobots.Models.Constants;

namespace MartianRobots.Logic.Services
{
    public class RectangularCommandExecuter : ICommandExecuter<RectangularMoveCommand>
    {
        public GridPosition Execute(GridPosition position, RectangularMoveCommand action)
        {
            switch (action)
            {
                case RectangularMoveCommand.Left:
                    return new GridPosition { X = position.X, Y = position.Y, Orientation = TurnLeft(position.Orientation) };

                case RectangularMoveCommand.Right:
                    return new GridPosition { X = position.X, Y = position.Y, Orientation = TurnRight(position.Orientation) };

                case RectangularMoveCommand.Forward:                    
                    var newPosition = MoveForward(position);
                    return new GridPosition { X = newPosition.Item1, Y = newPosition.Item2, Orientation = position.Orientation };

                default:
                    return position;
            };
        }

        private static OrientationState TurnLeft(OrientationState orientation)
        {
            if (orientation == OrientationState.North) return OrientationState.West;
            else
            {
                return orientation - 1;
            }
        }

        private static OrientationState TurnRight(OrientationState orientation)
        {
            if (orientation == OrientationState.West) return OrientationState.North;
            else
            {
                return orientation + 1;
            }
        }

        private static (int, int) MoveForward(GridPosition position)
        {
            return position.Orientation switch
            {
                OrientationState.North => (position.X, position.Y + 1),
                OrientationState.East => (position.X + 1, position.Y),
                OrientationState.South => (position.X, position.Y - 1),
                OrientationState.West => (position.X - 1, position.Y),
                _ => (position.X, position.Y),
            };
        }
    }
}
