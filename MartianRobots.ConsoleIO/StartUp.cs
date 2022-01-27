using Microsoft.Extensions.DependencyInjection;
using MartianRobots.ConsoleIO.Mappers;
using MartianRobots.Logic.Manager;
using MartianRobots.ConsoleIO.FileHandler;
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
