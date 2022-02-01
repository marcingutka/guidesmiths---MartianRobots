using MartianRobots.Data.Repositories;

namespace MartianRobots.Api.Services
{
    public class DeleteService : IDeleteService
    {
        private readonly IDataSetWriteRepository dataSetRepository;
        private readonly ISavedGridWriteRepository savedGridRepository;
        private readonly IRobotStepWriteRepository robotStepRepository;
        private readonly IInputDataWriteRepository inputDataRepository;

        public DeleteService(
            IDataSetWriteRepository dataSetRepository,
            ISavedGridWriteRepository savedGridRepository,
            IRobotStepWriteRepository robotStepRepository,
            IInputDataWriteRepository inputDataRepository
            )
        {
            this.dataSetRepository = dataSetRepository;
            this.savedGridRepository = savedGridRepository;
            this.robotStepRepository = robotStepRepository;
            this.inputDataRepository = inputDataRepository;
        }

        public async Task DeleteRunAsync(Guid runId)
        {
            var taskList = new List<Task>
            {
                dataSetRepository.DeleteDataSetAsync(runId),
                savedGridRepository.DeleteRunAsync(runId),
                robotStepRepository.DeleteRobotStepsAsync(runId),
                inputDataRepository.DeleteInputAsync(runId)
            };

            await Task.WhenAll(taskList);
        }
    }
}
