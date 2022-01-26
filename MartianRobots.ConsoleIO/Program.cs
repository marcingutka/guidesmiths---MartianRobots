using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.ConsoleIO;
using MartianRobots.ConsoleIO.DI;
using MartianRobots.Data.Entities;

var configuration = new ConfigurationBuilder()
    .AddJsonFile(@"F:\guidesmiths\MartianRobots.ConsoleIO\appsettings.json")
    .Build();

var services = new ServiceCollection();

DependencyInjection.CreateDependencies(services, configuration);

var provider = services.BuildServiceProvider();
var (DataNameWriteRepository, FileHandler, InputMapper, RobotManager) = StartUp.GetServices(provider);

//provide input for file path/console input
var fileName = "SampleAll.txt";
var filePath = @"F:\guidesmiths\sampleInputs\" + fileName;

var fileContent = FileHandler.ReadFile(filePath);

var (grid, robots, commands) = InputMapper.Map(fileContent);

RobotManager.AssignGridAndRobots(grid, robots.ToList(), commands.ToList());

var runId = await RobotManager.ExecuteTasksAsync();

await DataNameWriteRepository.SaveNameAsync(new DataName { RunId = runId, Name = fileName });

Console.WriteLine("*********** OUTPUT ***************");

foreach (var robot in robots)
{
    Console.WriteLine(robot.ToString());
}

FileHandler.WriteFile(robots.Select(x => x.ToString()), configuration.GetSection("OutputFile").GetSection("Path").Value + fileName.Replace(".txt", "- Results.txt"));