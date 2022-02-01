using Microsoft.Extensions.DependencyInjection;
using MartianRobots.FileHandler.Mappers;
using MartianRobots.FileHandler.Validator;

namespace MartianRobots.FileHandler.DI
{
    public class FileHandlerDI
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileHandler, TxtFileHandler>();
            services.AddSingleton<IInputMapper, InputMapper>();
            services.AddSingleton<IOutputFileMapper, OutputFileMapper>();
            services.AddSingleton<IInputValidator, InputValidator>();
        }
    }
}
