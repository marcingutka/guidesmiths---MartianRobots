using Microsoft.Extensions.DependencyInjection;
using MartianRobots.ConsoleIO.Mappers;
using LogicDI = MartianRobots.Logic.DI;
using MartianRobots.ConsoleIO.FileHandler;

namespace MartianRobots.ConsoleIO.DI
{
    public class DependencyInjection
    {
        public static void CreateDependencies(IServiceCollection services)
        {
            LogicDI.DependencyInjection.CreateDependencies(services);

            services.AddSingleton<IFileHandler, TxtFileHandler>();
            services.AddSingleton<IInputMapper, InputMapper>();
        }
    }
}
