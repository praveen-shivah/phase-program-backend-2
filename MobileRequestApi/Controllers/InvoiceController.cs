namespace MobileRequestApi.Controllers
{
    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using MobileRequestApiDTO;

    using Newtonsoft.Json;

    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : Controller
    {
        // private readonly IMessageSession messageSession;

        private readonly ILogger logger;

        public InvoiceController(ILogger logger)
        {
            // this.messageSession = messageSession;
            this.logger = logger;
        }

        [HttpPost("invoice-test")]
        public IActionResult InvoicePaid(Invoice information)
        {
            this.logger.Debug(LogClass.General, $"Invoice Paid {information}");

            return this.Ok();
        }

        [HttpPost("invoice-paid")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult InvoicePaid([FromForm]string JSONString)
        {
            this.logger.Debug(LogClass.General, "Invoice Paid");
            var invoice = JsonConvert.DeserializeObject<Root>(JSONString);

            return this.Ok();
        }
    }
}