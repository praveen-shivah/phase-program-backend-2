namespace MobileRequestApi.Controllers
{
    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using MobileRequestApiDTO;

    [ApiController]
    [Route("api/reseller")]
    public class ResellerController : Controller
    {
        private readonly ILogger logger;

        public ResellerController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpPost("reseller-balance")]
        public IActionResult ResellerBalance(ResellerBalance resellerBalance)
        {
            this.logger.Debug(LogClass.General, "ResellerBalance received");

            return this.Ok();
        }
    }
}