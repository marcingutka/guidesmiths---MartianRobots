using Microsoft.Extensions.DependencyInjection;
using MartianRobots.FileHandler.Mappers;
using MartianRobots.Logic.Manager;
using MartianRobots.FileHandler;
using MartianRobots.Data.Repositories;

namespace MartianRobots.ConsoleIO
{
    internal static class StartUp
    {
        internal static (IDataSetWriteRepository DataNameWriteRepository, IFileHandler FileHandler, IInputMapper InputMapper, IRobotManager RobotManager) GetServices(ServiceProvider provider)
        {
            var repository = provider.GetService<IDataSetWriteRepository>();
            var fileHandler = provider.GetService<IFileHandler>();
            var inputMapper = provider.GetService<IInputMapper>();
            var manager = provider.GetService<IRobotManager>();

            return (repository, fileHandler, inputMapper, manager);
        }
    }
}
