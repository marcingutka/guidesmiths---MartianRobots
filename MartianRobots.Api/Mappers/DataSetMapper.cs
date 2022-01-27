using MartianRobots.Data.Entities;
using MartianRobots.Api.Dto;

namespace MartianRobots.Api.Mappers
{
    public class DataSetMapper : IMapper<DataSet, DataSetDto>
    {
        public DataSet Map(DataSetDto dto)
        {
            return new DataSet
            {
                RunId = dto.RunId,
                Name = dto.Name,
                GenerationDate = dto.GenerationDate,
            };
        }

        public DataSetDto Map(DataSet entity)
        {
            return new DataSetDto
            {
                RunId = entity.RunId,
                Name = entity.Name,
                GenerationDate = entity.GenerationDate,
            };
        }
    }
}
