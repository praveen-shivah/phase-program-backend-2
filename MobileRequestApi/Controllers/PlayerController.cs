using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    using ApiDTO;
    using APISupport;
    using AuthenticationRepositoryTypes;
    using LoggingLibrary;
    using PlayersRepositoryTypes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/player")]
    public class PlayerController : Controller
    {
        private readonly ILogger logger;
        private readonly IPlayersRepository playersRepository;
        public PlayerController(ILogger logger, IPlayersRepository playersRepository)
        {
            this.logger = logger;
            this.playersRepository = playersRepository;
        }
        [HttpGet("get-player")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<ActionResult<List<PlayerDto>>> GetPlayers(int softwareType)
        {
            this.logger.Debug(LogClass.General, "GetPlayer received");

            var result = await this.playersRepository.GetPlayers(softwareType);
            return this.Ok(result);
        }
    }
}
