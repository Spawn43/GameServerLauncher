using Microsoft.EntityFrameworkCore;
using GameServerLauncher;
using GameServerLauncher.Services;
using GameServerLauncher.DbConnections;
using GameServerLauncher.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<INetworkService, NetworkService>();
builder.Services.AddSingleton<IRamService, RamService>();
builder.Services.AddSingleton<ICPUService, CPUService>();
builder.Services.AddTransient<IServerStatisticRepository, ServerStatisticsRepository>();

string connectionString = builder.Configuration.GetConnectionString("Defaultconnection");
builder.Services.AddSingleton<ISQLDbConnection>(provider => new SQLDbConnection(connectionString));

builder.Services.AddHostedService<ServerStatisticBackgroundService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
