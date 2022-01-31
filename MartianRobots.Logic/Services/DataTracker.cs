using MartianRobots.Models;
using MartianRobots.Models.Constants;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Logic.Services
{
    public class DataTracker : IDataTracker
    {
        private readonly IRobotStepWriteRepository writeRobotRepository;
        private readonly ISavedGridWriteRepository writeGridRepository;
        private readonly IDataSetWriteRepository writeDataSetRepository;

        private Guid RunId = Guid.Empty;
        private readonly List<RobotStep> trackedRobots = new();

        public DataTracker(
            IRobotStepWriteRepository writeRobotRepository,
            ISavedGridWriteRepository writeGridRepository,
            IDataSetWriteRepository writeDataSetRepository
            )
        {
            this.writeRobotRepository = writeRobotRepository;
            this.writeGridRepository = writeGridRepository;
            this.writeDataSetRepository = writeDataSetRepository;
        }

        public async Task SaveGridAsync(Grid grid)
        {
            var runId = Guid.NewGuid();

            while (!writeGridRepository.CheckRunId(runId))
            {
                runId = Guid.NewGuid();
            }

            RunId = runId;

            await writeGridRepository.SaveGridAsync(CreateSavedGrid(grid));
        }

        public async Task SaveRobotDataAsync()
        {
            if (RunId == Guid.Empty) throw new Exception("Run Id is not assigned.");

            await writeRobotRepository.SaveRobotMovement(trackedRobots);

            trackedRobots.Clear();
        }

        public async Task SaveRunNameAsync(string name, DateTime date)
        {
            if (RunId == Guid.Empty) throw new Exception("Run Id is not assigned.");

            var dataSet = new DataSet { RunId = RunId, Name = name, GenerationDate = date };

            await writeDataSetRepository.SaveNameAsync(dataSet);
        }

        public void CollectMetricData(int robotId, int stepNo, GridPosition position, RectangularMoveCommand? command = null, bool isLastStep = false, bool isLost = false)
        {
            if (RunId == Guid.Empty) throw new Exception("Run Id is not assigned.");

            var step = new RobotStep
            {
                RunId = RunId,
                RobotId = robotId,
                StepNumber = stepNo,
                Position = new Position { X = position.X, Y = position.Y },
                Orientation = position.Orientation,
                IsLost = isLost,
                IsLastStep = isLastStep,
                Command = command.HasValue ? command : null,
            };

            trackedRobots.Add(step);
        }

        private SavedGrid CreateSavedGrid(Grid grid)
        {
            return new SavedGrid
            {
                RunId = RunId,
                X = grid.X,
                Y = grid.Y,
            };
        }
    }
}
