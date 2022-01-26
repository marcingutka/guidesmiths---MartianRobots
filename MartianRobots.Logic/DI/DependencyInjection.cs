using Microsoft.Extensions.DependencyInjection;
using MartianRobots.Logic.Validators;
using MartianRobots.Logic.Manager;
using MartianRobots.Logic.Services;
using MartianRobots.Models.Constants;

namespace MartianRobots.Logic.DI
{
    public class DependencyInjection
    {
        public static void CreateDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPositionValidator, RectangularValidator>();
            services.AddSingleton<ICommandExecuter<RectangularMoveCommand>, RectangularCommandExecuter>();
            services.AddScoped<IRobotManager, RectangularRobotManager>();
        }
    }
}
