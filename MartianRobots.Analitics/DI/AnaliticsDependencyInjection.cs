using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Analitics.Mappers;
using MartianRobots.Analitics.Services;
using MartianRobots.Data.Entities;
using MartianRobots.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MartianRobots.Analitics.DI
{
    public static class AnaliticsDI
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            ConfigureMappers(services);
            ConfigureAnaliticsServices(services);
        }

        private static void ConfigureMappers(IServiceCollection services)
        {
            services.AddScoped<IMapper<RobotStep, LostRobot>, LostRobotMapper>();
            services.AddScoped<IMapper<Tuple<Position,int>, GridPoint>, GridPointMapper>();
        }

        private static void ConfigureAnaliticsServices(IServiceCollection services)
        {
            services.AddScoped<IGridAnaliticsService, GridAnaliticsService>();
        }
    }
}
