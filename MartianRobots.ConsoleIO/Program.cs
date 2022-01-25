using Microsoft.Extensions.DependencyInjection;
using MartianRobots.ConsoleIO.Mappers;
using MartianRobots.ConsoleIO.DI;
using MartianRobots.Logic.Manager;
using MartianRobots.ConsoleIO.FileHandler;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

DependencyInjection.CreateDependencies(services);

var provider = services.BuildServiceProvider();

//TODO: read input

var fileName = @"F:\guidesmiths\sampleInputs\SampleAll.txt";

var fileHandler = provider.GetService<IFileHandler>();
var fileContent = fileHandler.ReadFile(fileName);

var mapper = provider.GetService<IInputMapper>();

var grid = mapper.MapGrid(fileContent);

var robots = mapper.MapRobots(fileContent);

foreach (var robot in robots)
{
    Console.Write($"Id: { robot.Id }, X: {robot.Position.X}, Y: {robot.Position.Y}, Orient: {robot.Position.Orientation} Commands: ");
    foreach (var command in robot.Commands)
    {
        Console.Write(command + " ");
    }
    Console.Write("\n");
}

//TODO: do some work

var manager = provider.GetService<IRobotManager>();
manager.UploadGrid(grid);

foreach (var robot in robots)
{
    manager.ExecuteCommands(robot);
}

//TODO: write output

Console.WriteLine("\n\n*********** OUTPUT ***************");

foreach (var robot in robots)
{
    Console.WriteLine(robot.ToString());
}

//path to output in setting.json
fileHandler.WriteFile(robots.Select(x => x.ToString()), @"F:\guidesmiths\sampleInputs\output.txt");
