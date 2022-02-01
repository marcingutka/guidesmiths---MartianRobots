using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MartianRobots.ConsoleIO;
using MartianRobots.ConsoleIO.DI;

var configuration = new ConfigurationBuilder()
    .AddJsonFile(@"F:\guidesmiths\MartianRobots.ConsoleIO\appsettings.json")
    .Build();

var services = new ServiceCollection();

DependencyInjection.CreateDependencies(services, configuration);

var provider = services.BuildServiceProvider();
var (fileHandler, inputMapper, outputMapper, robotManager) = StartUp.GetServices(provider);

//provide input for file path/console input
var fileName = "SampleAll.txt";
var filePath = @"F:\guidesmiths\sampleInputs\" + fileName;

var fileContent = fileHandler.ReadFile(filePath);

var (grid, robots, commands) = inputMapper.Map(fileContent);

robotManager.AssignGridAndRobots(grid, robots, commands, fileName);

await robotManager.ExecuteTasksAsync();

Console.WriteLine("*********** OUTPUT ***************");

foreach (var robot in robots)
{
    Console.WriteLine(robot.ToString());
}

fileHandler.WriteFile(outputMapper.GenerateResults(robots.ToList()), configuration.GetSection("OutputFile").GetSection("Path").Value + fileName.Replace(".txt", "- Results.txt"));