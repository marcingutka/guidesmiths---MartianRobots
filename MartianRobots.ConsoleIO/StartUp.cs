﻿using Microsoft.Extensions.DependencyInjection;
using MartianRobots.FileHandler.Mappers;
using MartianRobots.Logic.Manager;
using MartianRobots.FileHandler;

namespace MartianRobots.ConsoleIO
{
    internal static class StartUp
    {
        internal static (IFileHandler FileHandler, IInputMapper InputMapper, IOutputFileMapper outputMapper, IRobotManager RobotManager) GetServices(ServiceProvider provider)
        {
            var fileHandler = provider.GetService<IFileHandler>();
            var inputMapper = provider.GetService<IInputMapper>();
            var outputMapper = provider.GetService<IOutputFileMapper>();
            var manager = provider.GetService<IRobotManager>();

            return (fileHandler, inputMapper, outputMapper, manager);
        }
    }
}
