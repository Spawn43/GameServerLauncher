using GameServerLauncher.Models;
using GameServerLauncher.Services;
using GameServerLauncher.Repository;
using System.Threading.Tasks;

public class ServerStatisticBackgroundService : BackgroundService
{

    readonly ILogger<ServerStatisticBackgroundService> _logger;
    readonly INetworkService _network;
    readonly IRamService _ram;
    readonly ICPUService _cpu;
    readonly IServerStatisticRepository _ssRepo;
    public ServerStatisticBackgroundService(ILogger<ServerStatisticBackgroundService> logger,
        INetworkService network, 
        IRamService ram, 
        ICPUService cpu, 
        IServerStatisticRepository ssRepo)
    {
        _logger = logger;
        _network = network;
        _ram = ram;
        _cpu = cpu;
        _ssRepo = ssRepo;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {


        _logger.LogInformation("Server Statistics Started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var serverStat = new ServerStatistic { Time = DateTime.Now, CPU = (float)Math.Round(_cpu.GetCpuUsage(), 2), RAMUsage = (float)Math.Round(_ram.CalculateRamUsage(), 2), RAMAmount = (float)Math.Round(_ram.CalculateRamTotal(), 2), Bandwidth = _network.GetNetworkUsage(), StorageAmount = 0f, StorageUsage = 0f };

            _logger.LogInformation("Worker Running at:{time}, CPU:{CPU}%, RAM Total:{RAMTotal}gb, RAM Usage:{RAMUsage}gb, Bandwidth:{time}mb/s", DateTimeOffset.Now, serverStat.CPU, serverStat.RAMAmount, serverStat.RAMUsage, serverStat.Bandwidth);

            _ssRepo.InsertServerStatstic(serverStat);
            await Task.Delay(10000, cancellationToken);
        }
    }
}