﻿using Microsoft.Extensions.DependencyInjection;
using MartianRobots.Analitics.AnaliticsModel;
using MartianRobots.Analitics.Mappers;
using MartianRobots.Analitics.Services;
using MartianRobots.Data.Entities;

namespace MartianRobots.Analitics.DI
{
    public static class AnaliticsDependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            ConfigureMappers(services);
            ConfigureAnaliticsServices(services);
        }

        private static void ConfigureMappers(IServiceCollection services)
        {
            services.AddScoped<IMapper<RobotStep, LostRobot>, LostRobotMapper>();
        }

        private static void ConfigureAnaliticsServices(IServiceCollection services)
        {
            services.AddScoped<IGridAnaliticsService, GridAnaliticsService>();
        }
    }
}
