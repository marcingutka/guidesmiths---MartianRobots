using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MartianRobots.Data.Entities;
using MartianRobots.Data.Providers;
using MartianRobots.Data.Repositories;

namespace MartianRobots.Data.DI
{
    public static class MongoDbDI
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();
            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(config.GetConnectionString("MongoConnection")));

            ConfigureDBProviders(services, config);
            ConfigureRepositories(services);
        }

        private static void ConfigureDBProviders(IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseConfig>(c => c.MongoDB = config.GetSection("Databases").GetSection("MongoDB").Value);
            services.AddSingleton<IDatabaseProvider<RobotStep>, DatabaseProvider<RobotStep>>();
            services.AddSingleton<IDatabaseProvider<SavedGrid>, DatabaseProvider<SavedGrid>>();
            services.AddSingleton<IDatabaseProvider<DataName>, DatabaseProvider<DataName>>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IRobotStepReadRepository, RobotStepReadRepository>();
            services.AddScoped<IRobotStepWriteRepository, RobotStepWriteRepository>();
            //services.AddScoped<ISavedGridReadRepository, SavedGridReadRepository>();
            services.AddScoped<ISavedGridWriteRepository, SavedGridWriteRepository>();
            services.AddScoped<IDataNameWriteRepository, DataNameWriteRepository>();
        }
    }
}
