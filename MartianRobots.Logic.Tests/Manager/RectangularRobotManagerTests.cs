using MartianRobots.Logic.Manager;
using MartianRobots.Logic.Services;
using MartianRobots.Logic.Validators;
using MartianRobots.Models;
using MartianRobots.Models.Constants;
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
        private IDataTracker dataTracker;

        private RectangularRobotManager manager;

        [SetUp]
        public void Setup()
        {
            cmdExecuter = Substitute.For<ICommandExecuter<RectangularMoveCommand>>();
            positionValidator = Substitute.For<IPositionValidator>();
            dataTracker = Substitute.For<IDataTracker>();

             manager = new RectangularRobotManager(cmdExecuter, positionValidator, dataTracker);
        }

        [Test]
        public async Task ExecuteTasksAsync_Assign_New_Position_When_Robot_Has_Not_Been_OffGrid()
        {
            //Arrange
            var robotId = 1;
            var runName = "TestRun";
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

            manager.AssignGridAndRobots(grid, robots, robotCommands, runName);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y + 1, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public async Task ExecuteTasksAsync_Do_Not_Assign_New_Position_When_Robot_Has_Been_OffGrid()
        {
            //Arrange
            var robotId = 1;
            var runName = "TestRun";
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
            positionValidator.IsRobotLost(default, default).ReturnsForAnyArgs(false);

            manager.AssignGridAndRobots(grid, robots, robotCommands, runName);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public async Task ExecuteTasksAsync_Do_Not_Assign_New_Position_When_Robot_Has_Been_Lost()
        {
            //Arrange
            var robotId = 1;
            var runName = "TestRun";
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

            manager.AssignGridAndRobots(grid, robots, robotCommands, runName);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            var currentPosition = robots.First(r => r.Id == robotId).Position;

            Assert.AreEqual(initialRobotPosition.X, currentPosition.X);
            Assert.AreEqual(initialRobotPosition.Y, currentPosition.Y);
            Assert.AreEqual(initialRobotPosition.Orientation, currentPosition.Orientation);
        }

        [Test]
        public async Task ExecuteTasksAsync_Set_IsLost_To_True_When_Robot_Has_Been_Lost()
        {
            //Arrange
            var robotId = 1;
            var runName = "TestRun";
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

            manager.AssignGridAndRobots(grid, robots, robotCommands, runName);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            Assert.IsTrue(robots.First(r => r.Id == robotId).IsLost);
        }

        [Test]
        public async Task ExecuteTasksAsync_Execute_Next_Command_When_Robot_Was_About_To_Be_OffGrid_But_Position_Was_Already_Marked()
        {
            //Arrange
            var robotId = 1;
            var secondRobotId = 2;
            var runName = "TestRun";
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
                },
                new Robot
                {
                    Id = secondRobotId,
                    Position = initialRobotPosition
                }
            };

            var commands = new List<RectangularMoveCommand>
            {
                RectangularMoveCommand.Forward,
                RectangularMoveCommand.Left
            };

            var robotCommands = new List<RobotCommands>
            {
                new RobotCommands
                {
                    Id = robotId,
                    Commands = commands
                },
                new RobotCommands
                {
                    Id = secondRobotId,
                    Commands = commands
                }
            };

            cmdExecuter.Execute(Arg.Any<GridPosition>(), Arg.Is<RectangularMoveCommand>(x => x == RectangularMoveCommand.Forward)).Returns(new GridPosition { X = 6, Y = 7, Orientation = OrientationState.North });
            cmdExecuter.Execute(Arg.Any<GridPosition>(), Arg.Is<RectangularMoveCommand>(x => x == RectangularMoveCommand.Left)).Returns(new GridPosition { X = 6, Y = 6, Orientation = OrientationState.West });
            positionValidator.IsRobotOffGrid(Arg.Is<GridPosition>(x => x.Orientation == OrientationState.North), Arg.Any<Grid>()).Returns(true);
            positionValidator.IsRobotOffGrid(Arg.Is<GridPosition>(x => x.Orientation == OrientationState.West), Arg.Any<Grid>()).Returns(false);
            positionValidator.IsRobotLost(Arg.Any<GridPosition>(), Arg.Is<List<Position>>(x => x.Count == 0)).Returns(true);
            positionValidator.IsRobotLost(Arg.Any<GridPosition>(), Arg.Is<List<Position>>(x => x.Count > 0)).Returns(false);

            manager.AssignGridAndRobots(grid, robots, robotCommands, runName);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            Assert.IsTrue(robots.First(r => r.Id == secondRobotId).Position.Orientation == OrientationState.West);
        }

        [Test]
        public async Task ExecuteTasksAsync_Do_Not_Assign_IsLost_When_Robot_Was_About_To_Be_OffGrid_But_Position_Was_Already_Marked()
        {
            //Arrange
            var robotId = 1;
            var secondRobotId = 2;
            var runName = "TestRun";
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
                },
                new Robot
                {
                    Id = secondRobotId,
                    Position = initialRobotPosition
                }
            };

            var commands = new List<RectangularMoveCommand>
            {
                RectangularMoveCommand.Forward,
                RectangularMoveCommand.Left
            };

            var robotCommands = new List<RobotCommands>
            {
                new RobotCommands
                {
                    Id = robotId,
                    Commands = commands
                },
                new RobotCommands
                {
                    Id = secondRobotId,
                    Commands = commands
                }
            };

            cmdExecuter.Execute(Arg.Any<GridPosition>(), Arg.Is<RectangularMoveCommand>(x => x == RectangularMoveCommand.Forward)).Returns(new GridPosition { X = 6, Y = 7, Orientation = OrientationState.North });
            cmdExecuter.Execute(Arg.Any<GridPosition>(), Arg.Is<RectangularMoveCommand>(x => x == RectangularMoveCommand.Left)).Returns(new GridPosition { X = 6, Y = 6, Orientation = OrientationState.West });
            positionValidator.IsRobotOffGrid(Arg.Is<GridPosition>(x => x.Orientation == OrientationState.North), Arg.Any<Grid>()).Returns(true);
            positionValidator.IsRobotOffGrid(Arg.Is<GridPosition>(x => x.Orientation == OrientationState.West), Arg.Any<Grid>()).Returns(false);
            positionValidator.IsRobotLost(Arg.Any<GridPosition>(), Arg.Is<List<Position>>(x => x.Count == 0)).Returns(true);
            positionValidator.IsRobotLost(Arg.Any<GridPosition>(), Arg.Is<List<Position>>(x => x.Count > 0)).Returns(false);

            manager.AssignGridAndRobots(grid, robots, robotCommands, runName);

            //Act
            await manager.ExecuteTasksAsync();

            //Assert
            Assert.IsFalse(robots.First(r => r.Id == secondRobotId).IsLost);
        }
    }
}