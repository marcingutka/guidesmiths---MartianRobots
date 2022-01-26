using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.ConsoleIO.Mappers;
using MartianRobots.ConsoleIO.DI;
using MartianRobots.Logic.Manager;
using MartianRobots.ConsoleIO.FileHandler;
using MartianRobots.Data.Repositories;
using MartianRobots.Data.Entities;

var config = new ConfigurationBuilder()
    .AddJsonFile(@"F:\guidesmiths\MartianRobots.ConsoleIO\appsettings.json")
    .Build();

var services = new ServiceCollection();

DependencyInjection.CreateDependencies(services, config);

var provider = services.BuildServiceProvider();
var repository = provider.GetService<IDataNameWriteRepository>();

//provide input for file path/console input
var fileName = "SampleAll.txt";
var filePath = @"F:\guidesmiths\sampleInputs\" + fileName;

var fileHandler = provider.GetService<IFileHandler>();
var fileContent = fileHandler.ReadFile(filePath);

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

var runId = await manager.ExecuteTasksAsync();

await repository.SaveNameAsync(new DataName { RunId = runId, Name = fileName});

Console.WriteLine("*********** OUTPUT ***************");

foreach (var robot in Robots)
{
    Console.WriteLine(robot.ToString());
}

fileHandler.WriteFile(Robots.Select(x => x.ToString()), config.GetSection("OutputFile").GetSection("Path").Value);
