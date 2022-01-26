using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.ConsoleIO;
using MartianRobots.ConsoleIO.DI;
using MartianRobots.Data.Entities;

var config = new ConfigurationBuilder()
    .AddJsonFile(@"F:\guidesmiths\MartianRobots.ConsoleIO\appsettings.json")
    .Build();

var services = new ServiceCollection();

DependencyInjection.CreateDependencies(services, config);

var provider = services.BuildServiceProvider();
var (DataNameWriteRepository, FileHandler, InputMapper, RobotManager) = StartUp.GetServices(provider);

//provide input for file path/console input
var fileName = "SampleAll.txt";
var filePath = @"F:\guidesmiths\sampleInputs\" + fileName;

var fileContent = FileHandler.ReadFile(filePath);

var (Grid, Robots, Commands) = InputMapper.Map(fileContent);

RobotManager.AssignGridAndRobots(Grid, Robots.ToList(), Commands.ToList());

var runId = await RobotManager.ExecuteTasksAsync();

await DataNameWriteRepository.SaveNameAsync(new DataName { RunId = runId, Name = fileName});

Console.WriteLine("*********** OUTPUT ***************");

foreach (var robot in Robots)
{
    Console.WriteLine(robot.ToString());
}

FileHandler.WriteFile(Robots.Select(x => x.ToString()), config.GetSection("OutputFile").GetSection("Path").Value + fileName.Replace(".txt", "- Results.txt"));