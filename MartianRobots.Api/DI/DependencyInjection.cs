using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Data.DI;
using MartianRobots.Data.Entities;

namespace MartianRobots.Api.DI
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            ConfigureApiServices(services);
            ConfigureMappers(services);

            MongoDbDI.ConfigureServices(services, config);
            services.AddControllers();
        }
        private static void ConfigureApiServices(IServiceCollection services)
        {
            //services.AddScoped<ICurrentGameReadService, CurrentGameReadService>();
            //services.AddScoped<ICurrentGameWriteService, CurrentGameWriteService>();
        }

        private static void ConfigureMappers(IServiceCollection services)
        {
            services.AddScoped<IMapper<DataSet, DataSetDto>, DataSetMapper>();
            services.AddScoped<IMapper<RobotStep, RobotStepDto>, RobotDtoMapper>();
        }
    }
}
