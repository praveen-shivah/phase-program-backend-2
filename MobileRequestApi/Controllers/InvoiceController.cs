namespace MobileRequestApi.Controllers
{
    using System;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using NServiceBus;

    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : Controller
    {
        private readonly IMessageSession messageSession;

        private readonly ILogger logger;

        public InvoiceController(ILogger logger)
        {
            // this.messageSession = messageSession;
            this.logger = logger;
        }

        [HttpPost("invoice-paid")]
        public IActionResult InvoicePaid(int invoiceId)
        {
            this.logger.Debug(LogClass.General, "Invoice Paid");

            return this.Ok();
        }
    }
}