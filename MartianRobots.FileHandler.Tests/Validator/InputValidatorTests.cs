using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MartianRobots.FileHandler.Validator;

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
        public void Validate_When_Grid_Input_Contains_Two_Numeric_Values_Separated_By_Space_And_Robot_Position_Input_Contains_Two_Numeric_Values_And_One_Orientation_Letter_Separated_By_Space_And_Robot_Commands_Input_Contains_Only_Commands_Letter_Do_Not_Throw_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "1 2";

            var robotInputPosition = "1 4 E";

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

            var robotInputPosition = "1 4 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }

        [Test]
        public void Validate_When_Grid_Input_Do_Not_Contain_Space_Throws_Exception()
        {
            //Arrange
            var content = new List<string>();

            var gridInput = "13";

            var robotInputPosition = "1 4 E";

            var robotInputCommands = "FRL";

            content.Add(gridInput);
            content.Add(robotInputPosition);
            content.Add(robotInputCommands);

            //Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(content));
        }
    }
}