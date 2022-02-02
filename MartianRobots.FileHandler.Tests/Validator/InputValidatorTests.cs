using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MartianRobots.FileHandler.Validator;
using MartianRobots.Models;

namespace MartianRobots.FileHandler.Tests.Validators
{
    public class InputValidatorTests
    {
        private InputValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new InputValidator();
        }

        [Test]
        public void Validate_When_Grid_Input_Contains_Two_Numeric_Values_Separated_By_Space_And_Robot_Position_Input_Contains_Two_Numeric_Values_And_One_Orientation_Letter_Separated_By_Space_And_Robot_Commands_Input_Contains_Only_Commands_Letter_Does_Not_Throw_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 2";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.DoesNotThrow(() => validator.Validate(content), "Correct input is not handled.");
        }

        [Test]
        public void Validate_When_Grid_Input_Contains_Alphabetic_Characters_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 A";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_There_Is_Any_Robots_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 A";

            content.Add(gridInput);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Commands_Are_Missed_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "13";

            var robotInputPosition = "1 2 E";

            content.Add(gridInput);
            content.Add(robotInputPosition);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Grid_Input_Do_Not_Contain_Space_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "13";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Grid_Input_Contains_3_Values_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3 5";

            var robotInputPosition = "1 2 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Robot_Position_Input_Contains_2_Letters_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "1 E E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Robot_Position_Input_Is_Not_Separated_By_Spaces_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "1 2E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Robot_Position_Input_Contains_One_Number_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "12 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Robot_Position_Input_Has_Letter_At_The_Beginning_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "W 2 1";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Robot_Position_Input_Has_Incorrect_Letter_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "2 2 T";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content), "'T' is considered as invalid robot Position");
        }

        [Test]
        public void Validate_When_Robot_Command_Input_Contains_Space_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "2 2 W";

            var robotInputCommands = "FR L";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Robot_Command_Input_Contains_Numeric_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 3";

            var robotInputPosition = "2 2 W";

            var robotInputCommands = "FR4L";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void CheckIfEachRobotStartsOnGrid_When_All_Robots_Are_Within_The_Grid_Does_Not_Throw_Exception()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var robots = new List<Robot>
            {
                new Robot {Position = new GridPosition {X = 1, Y = 1} },
                new Robot {Position = new GridPosition {X = 2, Y = 1} }
            };

            //Act & Assert
            Assert.DoesNotThrow(() => validator.CheckIfEachRobotStartsOnGrid(robots, grid));
        }

        [Test]
        public void CheckIfEachRobotStartsOnGrid_When_One_Of_The_Robots_Is_Outside_The_Grid_Throws_Exception()
        {
            //Arrange
            var grid = new Grid(3, 3);

            var robots = new List<Robot>
            {
                new Robot {Position = new GridPosition {X = 1, Y = 1} },
                new Robot {Position = new GridPosition {X = 4, Y = 1} }
            };

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.CheckIfEachRobotStartsOnGrid(robots, grid));
        }
    }
}