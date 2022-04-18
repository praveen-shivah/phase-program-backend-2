﻿namespace MobileRequestApi.Controllers
{
    using System.Threading.Tasks;

    using InvoiceRepositoryTypes;

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

        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceController(ILogger logger, IInvoiceRepository invoiceRepository)
        {
            // this.messageSession = messageSession;
            this.logger = logger;
            this.invoiceRepository = invoiceRepository;
        }

        [HttpPost("invoice-test")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult InvoiceTest([FromForm] string information)
        {
            this.logger.Debug(LogClass.General, $"Invoice Paid {information}");

            return this.Ok();
        }

        [HttpPost("invoice-paid")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> InvoicePaid([FromForm] string jsonString)
        {
            this.logger.Debug(LogClass.General, $"Invoice Paid {jsonString}");
            var response = await this.invoiceRepository.Store(new InvoiceStoreRequest(jsonString));

            return response.IsSuccessful ? this.Ok() : this.StatusCode(500, response.InvoiceStoreResponseType);
        }
    }
}