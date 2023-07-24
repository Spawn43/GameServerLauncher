using Microsoft.AspNetCore.Mvc;
using GameServerLauncher.Repository;
using Newtonsoft.Json;

namespace GameServerLauncher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerStatisticsController : ControllerBase
    {
        private readonly IServerStatisticRepository _serverStatisticRepository;

        public ServerStatisticsController(IServerStatisticRepository serverStatisticRepository)
        {
            _serverStatisticRepository = serverStatisticRepository;
        }

        [HttpGet]
        public async Task<String> Get(int minutes = 5)
        {
            var stats = await _serverStatisticRepository.GetServerStatsitic(minutes);
            return JsonConvert.SerializeObject(stats);
        }

  
    }
}
