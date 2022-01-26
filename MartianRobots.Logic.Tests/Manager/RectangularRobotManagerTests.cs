using MartianRobots.Logic.Manager;
using MartianRobots.Logic.Services;
using MartianRobots.Logic.Validators;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
using MartianRobots.Data.Repositories;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Logic.Tests
{
    public class RectangularRobotManagerTests
    {
        private ICommandExecuter<RectangularMoveCommand> cmdExecuter;
        private IPositionValidator positionValidator;
        private IRobotStepWriteRepository stepWriteRepository;
        private ISavedGridWriteRepository savedGridWriteRepository;

        private RectangularRobotManager manager;

        [SetUp]
        public void Setup()
        {
            cmdExecuter = Substitute.For<ICommandExecuter<RectangularMoveCommand>>();
            positionValidator = Substitute.For<IPositionValidator>();
            stepWriteRepository = Substitute.For<IRobotStepWriteRepository>();
            savedGridWriteRepository = Substitute.For<ISavedGridWriteRepository>();

            manager = new RectangularRobotManager(cmdExecuter, positionValidator, stepWriteRepository, savedGridWriteRepository);
        }

        [Test]
        public async Task Check_If_ExecuteTasksAsync_Assign_New_Position_When_Robot_Has_Not_Been_Lost()
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

            manager.AssignGridAndRobots(grid, robots, robotCommands);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y + 1, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public async Task Check_If_ExecuteTasksAsync_Do_Not_Assign_New_Position_When_Robot_Has_Been_Lost()
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

            manager.AssignGridAndRobots(grid, robots, robotCommands);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public async Task Check_If_ExecuteTasksAsync_Set_IsLost_To_True_When_Robot_Has_Been_Lost()
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

            manager.AssignGridAndRobots(grid, robots, robotCommands);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            Assert.IsTrue(robots.First(r => r.Id == robotId).IsLost);
        }
    }
}