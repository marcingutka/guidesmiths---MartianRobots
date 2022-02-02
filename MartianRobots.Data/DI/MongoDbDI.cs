using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;
using MartianRobots.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MartianRobots.Data.DI
{
    public static class MongoDbDI
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(config.GetConnectionString("MongoConnection")));

            ConfigureDBProviders(services, config);
            ConfigureRepositories(services);
        }

        private static void ConfigureDBProviders(IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseConfig>(c => c.MongoDB = config.GetSection("Databases").GetSection("MongoDB").Value);
            services.AddSingleton<IDatabaseProvider<RobotStep>, DatabaseProvider<RobotStep>>();
            services.AddSingleton<IDatabaseProvider<SavedGrid>, DatabaseProvider<SavedGrid>>();
            services.AddSingleton<IDatabaseProvider<DataSet>, DatabaseProvider<DataSet>>();
            services.AddSingleton<IDatabaseProvider<InputData>, DatabaseProvider<InputData>>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IRobotStepReadRepository, RobotStepReadRepository>();
            services.AddSingleton<IRobotStepWriteRepository, RobotStepWriteRepository>();
            services.AddSingleton<ISavedGridReadRepository, SavedGridReadRepository>();
            services.AddSingleton<ISavedGridWriteRepository, SavedGridWriteRepository>();
            services.AddSingleton<IDataSetReadRepository, DataSetReadRepository>();
            services.AddSingleton<IDataSetWriteRepository, DataSetWriteRepository>();
            services.AddSingleton<IInputDataReadRepository, InputDataReadRepository>();
            services.AddSingleton<IInputDataWriteRepository, InputDataWriteRepository>();
        }
    }
}
