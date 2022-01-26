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

var mappedData = mapper.Map(fileContent);

foreach (var robot in mappedData.Robots)
{
    Console.Write($"Id: { robot.Id }, X: {robot.Position.X}, Y: {robot.Position.Y}, Orient: {robot.Position.Orientation} Commands: ");
    foreach (var command in mappedData.Commands)
    {
        Console.Write(command + " ");
    }
    Console.Write("\n");
}

//TODO: do some work

var manager = provider.GetService<IRobotManager>();
manager.AssignRobots(mappedData.Grid, mappedData.Robots.ToList(), mappedData.Commands.ToList());

manager.ExecuteTasks();

//TODO: write output

Console.WriteLine("\n\n*********** OUTPUT ***************");

foreach (var robot in mappedData.Robots)
{
    Console.WriteLine(robot.ToString());
}

//path to output in setting.json
fileHandler.WriteFile(mappedData.Robots.Select(x => x.ToString()), @"F:\guidesmiths\sampleInputs\output.txt");
