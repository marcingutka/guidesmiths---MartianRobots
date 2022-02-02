using MartianRobots.Data.DI;
using MartianRobots.Logic.Manager;
using MartianRobots.Logic.Services;
using MartianRobots.Logic.Validators;
using MartianRobots.Models.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
