using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;
using MartianRobots.FileHandler;
using MartianRobots.FileHandler.Mappers;
using MartianRobots.Logic.Manager;
using MartianRobots.Models;

namespace MartianRobots.Api.Services
{
    public class UploadFileRunner : IUploadFileRunner
    {
        private readonly IFileHandler fileHandler;
        private readonly IInputMapper mapper;
        private readonly IRobotManager manager;
        private readonly IInputDataWriteRepository repository;

        public UploadFileRunner(
            IFileHandler fileHandler,
            IInputMapper mapper,
            IRobotManager manager,
            IInputDataWriteRepository repository
            )
        {
            this.fileHandler = fileHandler;
            this.mapper = mapper;
            this.manager = manager;
            this.repository = repository;
        }

        public async Task RunFile(IFormFile file, string runName)
        {
            var fileContent = fileHandler.ReadFile(file);
            var (grid, robots, command) = mapper.Map(fileContent);

            var initialRobots = SaveInitialRobots(robots.ToList());            

            manager.AssignGridAndRobots(grid, robots, command, runName);
            var runId = await manager.ExecuteTasksAsync();

            var inputData = GenerateInputData(runId, grid, initialRobots, command);
            await repository.SaveInputAsync(inputData);
        }

        private static List<Robot> SaveInitialRobots(List<Robot> robots)
        {
            var initialRobots = new List<Robot>();

            foreach (var robot in robots)
            {
                initialRobots.Add(new Robot
                {
                    Id = robot.Id,
                    Position = new GridPosition { X = robot.Position.X, Y = robot.Position.Y, Orientation = robot.Position.Orientation }
                });
            }

            return initialRobots;
        }

        private static InputData GenerateInputData(Guid runId, Grid grid, List<Robot> robots, IEnumerable<RobotCommands> commands)
        {
            return new InputData
            {
                RunId = runId,
                Grid = grid,
                Robots = robots,
                Commands = commands.ToList()
            };
        }
    }
}
