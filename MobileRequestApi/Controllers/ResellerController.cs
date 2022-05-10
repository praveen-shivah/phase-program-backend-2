namespace MobileRequestApi.Controllers
{
    using System.Threading.Tasks;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using MobileRequestApiDTO;

    using ResellerRepository;

    [ApiController]
    [Route("api/reseller")]
    public class ResellerController : Controller
    {
        private readonly ILogger logger;

        private readonly IResellerBalanceService resellerBalanceService;

        public ResellerController(ILogger logger, IResellerBalanceService resellerBalanceService)
        {
            this.logger = logger;
            this.resellerBalanceService = resellerBalanceService;
        }

        [HttpPost("reseller-balance")]
        public async Task<IActionResult> ResellerBalance(ResellerBalanceDTO resellerBalance)
        {
            this.logger.Debug(LogClass.General, "ResellerBalance received");
            var result = await this.resellerBalanceService.UpdateBalance(resellerBalance);
            return result ? this.Ok() : this.StatusCode(500, 0);
        }
    }
}