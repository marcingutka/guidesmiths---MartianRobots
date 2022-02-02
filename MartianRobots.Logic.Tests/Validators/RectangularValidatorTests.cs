using MartianRobots.Logic.Validators;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using NUnit.Framework;
using System.Collections.Generic;

namespace MartianRobots.Logic.Tests.Validators
{
    public class RectangularValidatorTests
    {
        private RectangularValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new RectangularValidator();
        }

        [Test]
        public void IsRobotOffGrid_When_X_Is_Greater_Than_Grid_X_Returns_True()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var position = new Position
            {
                X = 4,
                Y = 3
            };

            //Act
            var result = validator.IsRobotOffGrid(position, grid);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRobotOffGrid_When_X_Is_Less_Than_0_Returns_True()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var position = new Position
            {
                X = -1,
                Y = 3
            };

            //Act
            var result = validator.IsRobotOffGrid(position, grid);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRobotOffGrid_When_Y_Is_Greater_Than_Grid_Y_Returns_True()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var position = new Position
            {
                X = 3,
                Y = 4
            };

            //Act
            var result = validator.IsRobotOffGrid(position, grid);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRobotOffGrid_When_Y_Is_Less_Than_0_Returns_True()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var position = new Position
            {
                X = 3,
                Y = -1
            };

            //Act
            var result = validator.IsRobotOffGrid(position, grid);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRobotOffGrid_When_X_Is_Greater_Or_Equal_Than_0_And_X_Is_Less_Or_Equal_Than_Grid_X_And_Y_Is_Greater_Or_Equal_Than_0_And_Y_Is_Less_Or_Equal_Than_Grid_Y_Returns_False()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var position = new Position
            {
                X = 3,
                Y = 2
            };

            //Act
            var result = validator.IsRobotOffGrid(position, grid);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsRobotLost_When_Position_Is_Not_In_The_List_Returns_True()
        {
            //Arrange
            var positionList = new List<Position>()
            {
                new Position {X = 3, Y = 2},
            };

            var position = new Position
            {
                X = 3,
                Y = 3
            };

            //Act
            var result = validator.IsRobotLost(position, positionList);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRobotLost_When_Position_Is_In_The_List_Returns_False()
        {
            //Arrange
            var positionList = new List<Position>()
            {
                new Position {X = 3, Y = 3},
            };

            var position = new Position
            {
                X = 3,
                Y = 3
            };

            //Act
            var result = validator.IsRobotLost(position, positionList);

            //Assert
            Assert.IsTrue(result);
        }
    }
}