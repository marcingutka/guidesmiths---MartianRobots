using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.FileHandler.DI;
using LogicDI = MartianRobots.Logic.DI;

namespace MartianRobots.ConsoleIO.DI
{
    public class DependencyInjection
    {
        public static void CreateDependencies(IServiceCollection services, IConfiguration config)
        {
            LogicDI.DependencyInjection.CreateDependencies(services, config);
            FileHandlerDI.CreateDependencies(services);
        }
    }
}
