using MartianRobots.Logic.Services;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using NUnit.Framework;

namespace MartianRobots.Logic.Tests.Services
{
    public class RectangularCommandExecuterTests
    {
        private RectangularCommandExecuter commandExecuter;

        [SetUp]
        public void Setup()
        {
            commandExecuter = new RectangularCommandExecuter();
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_East_And_Command_Is_Equal_Forward_Returns_Increased_X_By_1()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.East
            };

            var command = RectangularMoveCommand.Forward;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(4, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.East, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_West_And_Command_Is_Equal_Forward_Returns_Decreased_X_By_1()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.West
            };

            var command = RectangularMoveCommand.Forward;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.West, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_North_And_Command_Is_Equal_Forward_Returns_Increased_Y_By_1()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.North
            };

            var command = RectangularMoveCommand.Forward;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(4, result.Y);
            Assert.AreEqual(OrientationState.North, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_South_And_Command_Is_Equal_Forward_Returns_Decreased_Y_By_1()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.South
            };

            var command = RectangularMoveCommand.Forward;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(2, result.Y);
            Assert.AreEqual(OrientationState.South, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_North_And_Command_Is_Equal_Left_Returns_Orientation_Equals_West()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.North
            };

            var command = RectangularMoveCommand.Left;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.West, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_West_And_Command_Is_Equal_Left_Returns_Orientation_Equals_South()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.West
            };

            var command = RectangularMoveCommand.Left;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.South, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_South_And_Command_Is_Equal_Left_Returns_Orientation_Equals_East()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.South
            };

            var command = RectangularMoveCommand.Left;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.East, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_East_And_Command_Is_Equal_Left_Returns_Orientation_Equals_North()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.East
            };

            var command = RectangularMoveCommand.Left;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.North, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_North_And_Command_Is_Equal_Right_Returns_Orientation_Equals_East()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.North
            };

            var command = RectangularMoveCommand.Right;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.East, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_East_And_Command_Is_Equal_Right_Returns_Orientation_Equals_South()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.East
            };

            var command = RectangularMoveCommand.Right;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.South, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_South_And_Command_Is_Equal_Right_Returns_Orientation_Equals_West()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.South
            };

            var command = RectangularMoveCommand.Right;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.West, result.Orientation);
        }

        [Test]
        public void Execute_When_Orientation_Is_Equal_West_And_Command_Is_Equal_Right_Returns_Orientation_Equals_North()
        {
            //Arrange
            var initialPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.West
            };

            var command = RectangularMoveCommand.Right;

            //Act
            var result = commandExecuter.Execute(initialPosition, command);

            //Assert
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(3, result.Y);
            Assert.AreEqual(OrientationState.North, result.Orientation);
        }
    }
}