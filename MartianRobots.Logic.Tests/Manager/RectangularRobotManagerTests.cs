using MartianRobots.Logic.Manager;
using MartianRobots.Logic.Services;
using MartianRobots.Logic.Validators;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Logic.Tests
{
    public class RectangularRobotManagerTests
    {
        private ICommandExecuter<RectangularMoveCommand> cmdExecuter;
        private IPositionValidator positionValidator;

        private RectangularRobotManager manager;

        [SetUp]
        public void Setup()
        {
            cmdExecuter = Substitute.For<ICommandExecuter<RectangularMoveCommand>>();
            positionValidator = Substitute.For<IPositionValidator>();

            manager = new RectangularRobotManager(cmdExecuter, positionValidator);
        }

        [Test]
        public void Check_If_ExecuteTasks_Assign_New_Position_When_Robot_Has_Not_Been_Lost()
        {
            //Arrange
            var robotId = 1;

            var grid = new Grid(6, 6);
            var initialRobotPosition = new GridPosition()
            {
                X = 3,
                Y = 3,
                Orientation = OrientationState.North
            };

            var robots = new List<Robot>
            {
                new Robot
                {
                    Id = robotId,
                    Position = initialRobotPosition
                }
            };

            var commands = new List<RectangularMoveCommand>
            {
                RectangularMoveCommand.Forward
            };

            var robotCommands = new List<RobotCommands>
            {
                new RobotCommands
                {
                    Id = robotId,
                    Commands = commands
                }
            };

            cmdExecuter.Execute(default, default).ReturnsForAnyArgs(new GridPosition { X = 3, Y = 4, Orientation = OrientationState.North});
            positionValidator.IsRobotOffGrid(default, default).ReturnsForAnyArgs(false);

            manager.AssignRobots(grid, robots, robotCommands);

            //Act
            manager.ExecuteTasks();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y + 1, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public void Check_If_ExecuteTasks_Do_Not_Assign_New_Position_When_Robot_Has_Been_Lost()
        {
            //Arrange
            var robotId = 1;

            var grid = new Grid(6, 6);
            var initialRobotPosition = new GridPosition()
            {
                X = 6,
                Y = 6,
                Orientation = OrientationState.North
            };

            var robots = new List<Robot>
            {
                new Robot
                {
                    Id = robotId,
                    Position = initialRobotPosition
                }
            };

            var commands = new List<RectangularMoveCommand>
            {
                RectangularMoveCommand.Forward
            };

            var robotCommands = new List<RobotCommands>
            {
                new RobotCommands
                {
                    Id = robotId,
                    Commands = commands
                }
            };

            cmdExecuter.Execute(default, default).ReturnsForAnyArgs(new GridPosition { X = 6, Y = 7, Orientation = OrientationState.North });
            positionValidator.IsRobotOffGrid(default, default).ReturnsForAnyArgs(true);
            positionValidator.IsRobotLost(default, default).ReturnsForAnyArgs(true);

            manager.AssignRobots(grid, robots, robotCommands);

            //Act
            manager.ExecuteTasks();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public void Check_If_ExecuteTasks_Set_IsLost_To_True_When_Robot_Has_Been_Lost()
        {
            //Arrange
            var robotId = 1;

            var grid = new Grid(6, 6);
            var initialRobotPosition = new GridPosition()
            {
                X = 6,
                Y = 6,
                Orientation = OrientationState.North
            };

            var robots = new List<Robot>
            {
                new Robot
                {
                    Id = robotId,
                    Position = initialRobotPosition
                }
            };

            var commands = new List<RectangularMoveCommand>
            {
                RectangularMoveCommand.Forward
            };

            var robotCommands = new List<RobotCommands>
            {
                new RobotCommands
                {
                    Id = robotId,
                    Commands = commands
                }
            };

            cmdExecuter.Execute(default, default).ReturnsForAnyArgs(new GridPosition { X = 6, Y = 7, Orientation = OrientationState.North });
            positionValidator.IsRobotOffGrid(default, default).ReturnsForAnyArgs(true);
            positionValidator.IsRobotLost(default, default).ReturnsForAnyArgs(true);

            manager.AssignRobots(grid, robots, robotCommands);

            //Act
            manager.ExecuteTasks();

            //Assert
            Assert.IsTrue(robots.First(r => r.Id == robotId).IsLost);
        }
    }
}