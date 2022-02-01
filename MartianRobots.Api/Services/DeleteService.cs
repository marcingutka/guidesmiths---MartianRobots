using MartianRobots.Data.Repositories;

namespace MartianRobots.Api.Services
{
    public class DeleteService : IDeleteService
    {
        private readonly IDataSetWriteRepository dataSetRepository;
        private readonly ISavedGridWriteRepository savedGridRepository;
        private readonly IRobotStepWriteRepository robotStepRepository;

        public DeleteService(
            IDataSetWriteRepository dataSetRepository,
            ISavedGridWriteRepository savedGridRepository,
            IRobotStepWriteRepository robotStepRepository
            )
        {
            this.dataSetRepository = dataSetRepository;
            this.savedGridRepository = savedGridRepository;
            this.robotStepRepository = robotStepRepository;
        }

        public async Task DeleteRunAsync(Guid runId)
        {
            var taskList = new List<Task>
            {
                dataSetRepository.DeleteDataSetAsync(runId),
                savedGridRepository.DeleteRunAsync(runId),
                robotStepRepository.DeleteRobotStepsAsync(runId)
            };

            await Task.WhenAll(taskList);
        }
    }
}
