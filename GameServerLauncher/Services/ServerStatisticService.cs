using GameServerLauncher.Models;
using GameServerLauncher.Services;

public class ServerStatisticService : BackgroundService
{

    readonly ILogger<ServerStatisticService> _logger;
    readonly INetworkService _network;
    readonly IRamService _ram;
    readonly ICPUService _cpu;
    public ServerStatisticService(ILogger<ServerStatisticService> logger, INetworkService network, IRamService ram, ICPUService cpu)
    {
        _logger = logger;
        _network = network;
        _ram = ram;
        _cpu = cpu;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {


        _logger.LogInformation("Server Statistics Started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var serverStat = new ServerStatistic { Time = DateTime.Now, CPU = (float)Math.Round(_cpu.GetCpuUsage(), 2), RAMUsage = (float)Math.Round(_ram.CalculateRamUsage(), 2), RAMAmount = (float)Math.Round(_ram.CalculateRamTotal(), 2), Bandwidth = _network.GetNetworkUsage() };

            _logger.LogInformation("Worker Running at:{time}, CPU:{CPU}%, RAM Total:{RAMTotal}gb, RAM Usage:{RAMUsage}gb, Bandwidth:{time}mb/s", DateTimeOffset.Now, serverStat.CPU, serverStat.RAMAmount, serverStat.RAMUsage, serverStat.Bandwidth);
            await Task.Delay(1000, cancellationToken);
        }
    }
}