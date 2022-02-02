using MartianRobots.FileHandler.Mappers;
using MartianRobots.FileHandler.Validator;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.FileHandler.Tests.Mappers
{
    public class InputMapperTests
    {
        private IInputValidator validator;
        private InputMapper mapper;

        [SetUp]
        public void Setup()
        {
            validator = Substitute.For<IInputValidator> ();

            mapper = new InputMapper(validator);
        }

        [Test]
        public void Map_When_Grid_Robot_Position_Robot_Commands_Are_Correct_Returns_Grid()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 2";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            validator.Validate(default).ReturnsForAnyArgs(true);
            validator.CheckIfEachRobotStartsOnGrid(default, default).ReturnsForAnyArgs(true);

            //Act            
            var (grid, _, _) = mapper.Map(content);

            //Assert
            Assert.AreEqual(new Grid(1, 2), grid);
        }

        [Test]
        public void Map_When_Grid_Robot_Position_Robot_Commands_Are_Correct_Returns_Robot_Position()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 2";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            validator.Validate(default).ReturnsForAnyArgs(true);
            validator.CheckIfEachRobotStartsOnGrid(default, default).ReturnsForAnyArgs(true);

            //Act            
            var (_, robots, _) = mapper.Map(content);

            //Assert
            var robot = robots.FirstOrDefault();
            Assert.AreEqual(1, robot.Position.X);
            Assert.AreEqual(2, robot.Position.Y);
            Assert.AreEqual(OrientationState.East, robot.Position.Orientation);
        }

        [Test]
        public void Map_When_Grid_Robot_Position_Robot_Commands_Are_Correct_Returns_Robot_Commands()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 2";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            validator.Validate(default).ReturnsForAnyArgs(true);
            validator.CheckIfEachRobotStartsOnGrid(default, default).ReturnsForAnyArgs(true);

            //Act            
            var (_, _, commands) = mapper.Map(content);

            //Assert
            var command = commands.FirstOrDefault();
            Assert.AreEqual(RectangularMoveCommand.Forward, command.Commands[0]);
            Assert.AreEqual(RectangularMoveCommand.Right, command.Commands[1]);
            Assert.AreEqual(RectangularMoveCommand.Left, command.Commands[2]);
        }
    }
}