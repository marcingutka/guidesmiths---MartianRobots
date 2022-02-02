using MartianRobots.Api.Dto;
using MartianRobots.Data.Entities;
using MartianRobots.Models;

namespace MartianRobots.Api.Mappers
{
    public class RobotDtoMapper : IMapper<RobotStep, RobotStepDto>
    {
        public RobotStep Map(RobotStepDto dto)
        {
            return new RobotStep
            {
                RobotId = dto.Id,
                StepNumber = dto.StepNumber,
                Position = new GridPosition
                { 
                    X = dto.X, 
                    Y = dto.Y,
                    Orientation = dto.Orientation,
                },
                Command = dto.Command,
                IsLost = dto.IsLost,
            };
        }

        public RobotStepDto Map(RobotStep entity)
        {
            return new RobotStepDto
            {
                Id = entity.RobotId,
                StepNumber = entity.StepNumber,
                X = entity.Position.X,
                Y = entity.Position.Y,
                Orientation = entity.Orientation,
                Command = entity.Command,
                IsLost= entity.IsLost,
            };
        }
    }
}
