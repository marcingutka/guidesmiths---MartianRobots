using Microsoft.Extensions.DependencyInjection;
using MartianRobots.FileHandler.Mappers;

namespace MartianRobots.FileHandler.DI
{
    public class FileHandlerDI
    {
        public static void CreateDependencies(IServiceCollection services)
        {
            services.AddSingleton<IFileHandler, TxtFileHandler>();
            services.AddSingleton<IInputMapper, InputMapper>();
        }
    }
}
