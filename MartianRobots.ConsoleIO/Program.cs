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
var (dataNameWriteRepository, fileHandler, inputMapper, robotManager) = StartUp.GetServices(provider);

//check db connection -> https://stackoverflow.com/questions/28835833/how-to-check-connection-to-mongodb

//provide input for file path/console input
var fileName = "SampleAll.txt";
var filePath = @"F:\guidesmiths\sampleInputs\" + fileName;

var fileContent = fileHandler.ReadFile(filePath);

var (grid, robots, commands) = inputMapper.Map(fileContent);

robotManager.AssignGridAndRobots(grid, robots, commands);

var runId = await robotManager.ExecuteTasksAsync();

await dataNameWriteRepository.SaveNameAsync(new DataName { RunId = runId, Name = fileName });

Console.WriteLine("*********** OUTPUT ***************");

foreach (var robot in robots)
{
    Console.WriteLine(robot.ToString());
}

fileHandler.WriteFile(robots.Select(x => x.ToString()), configuration.GetSection("OutputFile").GetSection("Path").Value + fileName.Replace(".txt", "- Results.txt"));