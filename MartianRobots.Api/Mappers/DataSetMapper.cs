using System.Globalization;
using MartianRobots.Data.Entities;
using MartianRobots.Api.Dto;

namespace MartianRobots.Api.Mappers
{
    public class DataSetMapper : IMapper<DataSet, DataSetDto>
    {
        private const string DATE_FORMAT = "dd/MM/yyyy hh:mm:ss";
        public DataSet Map(DataSetDto dto)
        {
            return new DataSet
            {
                RunId = dto.RunId,
                Name = dto.Name,
                GenerationDate = DateTime.ParseExact(dto.GenerationDate, DATE_FORMAT, CultureInfo.InvariantCulture),
            };
        }

        public DataSetDto Map(DataSet entity)
        {
            return new DataSetDto
            {
                RunId = entity.RunId,
                Name = entity.Name,
                GenerationDate = entity.GenerationDate.ToString(DATE_FORMAT, CultureInfo.InvariantCulture),
            };
        }
    }
}
