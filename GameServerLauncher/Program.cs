using Microsoft.EntityFrameworkCore;
using GameServerLauncher.Data;
using GameServerLauncher;
using GameServerLauncher.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<INetworkService, NetworkService>();
builder.Services.AddSingleton<IRamService, RamService>();
builder.Services.AddSingleton<ICPUService, CPUService>();

builder.Services.AddHostedService<ServerStatisticService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
