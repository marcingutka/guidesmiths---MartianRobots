using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.ConsoleIO.Mappers;
using MartianRobots.ConsoleIO.FileHandler;
using LogicDI = MartianRobots.Logic.DI;

namespace MartianRobots.ConsoleIO.DI
{
    public class DependencyInjection
    {
        public static void CreateDependencies(IServiceCollection services, IConfiguration config)
        {
            LogicDI.DependencyInjection.CreateDependencies(services, config);

            services.AddSingleton<IFileHandler, TxtFileHandler>();
            services.AddSingleton<IInputMapper, InputMapper>();
        }
    }
}
