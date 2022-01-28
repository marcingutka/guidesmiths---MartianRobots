﻿using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;
using MartianRobots.FileHandler;
using MartianRobots.FileHandler.Mappers;
using MartianRobots.Logic.Manager;

namespace MartianRobots.Api.Services
{
    public class UploadFileRunner : IUploadFileRunner
    {
        private readonly IFileHandler fileHandler;
        private readonly IInputMapper mapper;
        private readonly IRobotManager manager;
        private readonly IDataSetWriteRepository repository;

        public UploadFileRunner(
            IFileHandler fileHandler,
            IInputMapper mapper,
            IRobotManager manager,
            IDataSetWriteRepository repository
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
            manager.AssignGridAndRobots(grid, robots, command);

            var runId = await manager.ExecuteTasksAsync();

            await repository.SaveNameAsync(new DataSet { RunId = runId, Name = runName, GenerationDate = DateTime.UtcNow });
        }
    }
}
