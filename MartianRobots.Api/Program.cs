using MartianRobots.Api.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt => opt.AddPolicy(name: "FrontClient", builder =>
{
    builder.WithOrigins("http://localhost:3000").WithMethods("GET", "POST", "DELETE").WithHeaders("Access-Control-Allow-Origin");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
