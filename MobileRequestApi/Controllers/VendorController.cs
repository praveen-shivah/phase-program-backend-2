namespace MobileRequestApi.Controllers
{
    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/vendor")]
    public class VendorController : Controller
    {
        private readonly ILogger logger;

        public VendorController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpPost("vendor-balance")]
        public IActionResult VendorBalance(int operatorId, string balance)
        {
            this.logger.Debug(LogClass.General, "VendorBalance received");

            return this.Ok();
        }
    }
}