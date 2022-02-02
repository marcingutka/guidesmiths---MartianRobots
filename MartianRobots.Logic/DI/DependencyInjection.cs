using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.Logic.Validators;
using MartianRobots.Logic.Manager;
using MartianRobots.Logic.Services;
using MartianRobots.Models.Constants;
using MartianRobots.Data.DI;

namespace MartianRobots.Logic.DI
{
    public class LogicDI
    {
        public static void CreateDependencies(IServiceCollection services, IConfiguration config)
        {
            MongoDbDI.ConfigureServices(services, config);

            services.AddSingleton<IPositionValidator, RectangularValidator>();
            services.AddSingleton<ICommandExecuter<RectangularMoveCommand>, RectangularCommandExecuter>();
            services.AddScoped<IDataTracker, DataTracker>();
            services.AddScoped<IRobotManager, RectangularRobotManager>();
        }
    }
}
