namespace ApiHost
{
    using System.Threading.Tasks;

    using ApiHost;
    using ApiHost.Middleware;
    using APISupport;
    using AuthenticationRepositoryTypes;

    using InvoiceRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ApiControllerBase
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

            var response = await this.invoiceRepository.Store(new InvoiceStoreRequest(this.OrganizationId, jsonString));
            return response.IsSuccessful ? this.Ok() : this.StatusCode(500, response.InvoiceStoreResponseType);
        }
    }
}