using MartianRobots.Data.Repositories;
using MartianRobots.Data.Entities;
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

        public async Task RunFile(string path, string runName)
        {
            var fileContent = fileHandler.ReadFile(path);
            var (grid, robots, command) = mapper.Map(fileContent);

            manager.AssignGridAndRobots(grid, robots, command, runName);
            var runId = await manager.ExecuteTasksAsync();

            var inputData = GenerateInputData(runId, grid, robots, command, runName);
            await repository.SaveInput(inputData);
        }

        private InputData GenerateInputData(Guid runId, Grid grid, IEnumerable<Robot> robots, IEnumerable<RobotCommands> commands, string runName)
        {
            return new InputData
            {
                RunId = runId,
                Grid = grid,
                Robots = robots,
                Commands = commands,
                Name = runName
            };
        }
    }
}
