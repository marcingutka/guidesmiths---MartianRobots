using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.ConsoleIO.Mappers;
using MartianRobots.ConsoleIO.DI;
using MartianRobots.Logic.Manager;
using MartianRobots.ConsoleIO.FileHandler;

var config = new ConfigurationBuilder()
    .AddJsonFile(@"F:\guidesmiths\MartianRobots.ConsoleIO\appsettings.json")
    .Build();

var services = new ServiceCollection();

DependencyInjection.CreateDependencies(services, config);

var provider = services.BuildServiceProvider();

//provide input for file path/console input
var fileName = @"F:\guidesmiths\sampleInputs\SampleAll.txt";

var fileHandler = provider.GetService<IFileHandler>();
var fileContent = fileHandler.ReadFile(fileName);

var (Grid, Robots, Commands) = provider.GetService<IInputMapper>().Map(fileContent);

/*foreach (var robot in mappedData.Robots)
{
    Console.Write($"Id: { robot.Id }, X: {robot.Position.X}, Y: {robot.Position.Y}, Orient: {robot.Position.Orientation} Commands: ");
    foreach (var command in mappedData.Commands)
    {
        Console.Write(command + " ");
    }
    Console.Write("\n");
}*/

var manager = provider.GetService<IRobotManager>();
manager.AssignRobots(Grid, Robots.ToList(), Commands.ToList());

await manager.ExecuteTasksAsync();

Console.WriteLine("*********** OUTPUT ***************");

foreach (var robot in Robots)
{
    Console.WriteLine(robot.ToString());
}

fileHandler.WriteFile(Robots.Select(x => x.ToString()), config.GetSection("OutputFile").GetSection("Path").Value);
