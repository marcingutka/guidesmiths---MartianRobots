using MartianRobots.Analitics.DI;
using MartianRobots.Api.Dto;
using MartianRobots.Api.Mappers;
using MartianRobots.Api.Services;
using MartianRobots.Data.DI;
using MartianRobots.Data.Entities;
using MartianRobots.FileHandler.DI;
using MartianRobots.Logic.DI;

namespace MartianRobots.Api.DI
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            ConfigureApiServices(services);
            ConfigureMappers(services);

            AnaliticsDI.ConfigureServices(services);
            MongoDbDI.ConfigureServices(services, config);
            FileHandlerDI.ConfigureServices(services);
            LogicDI.CreateDependencies(services, config);

            services.AddControllers();
        }
        private static void ConfigureApiServices(IServiceCollection services)
        {
            services.AddScoped<IUploadFileRunner, UploadFileRunner>();
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IDeleteService, DeleteService>();
        }

        private static void ConfigureMappers(IServiceCollection services)
        {
            services.AddScoped<IMapper<DataSet, DataSetDto>, DataSetMapper>();
            services.AddScoped<IMapper<RobotStep, RobotStepDto>, RobotDtoMapper>();
        }
    }
}
