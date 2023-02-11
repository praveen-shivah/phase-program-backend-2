namespace ApiHost
{
    using ApiDTO;

    using InvoiceRepository;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    [ApiController]
    [Route("api/invoicereports")]
    public class InvoiceReportController : ApiControllerBase
    {
        // private readonly IMessageSession messageSession;

        private readonly ILogger logger;

        private readonly IInvoiceListRetrieveRepository invoiceListRetrieveRepository;

        public InvoiceReportController(ILogger logger, IInvoiceListRetrieveRepository invoiceListRetrieveRepository)
        {
            // this.messageSession = messageSession;
            this.logger = logger;
            this.invoiceListRetrieveRepository = invoiceListRetrieveRepository;
        }

        [HttpPost("get-invoice-list")]
        public async Task<ActionResult<InvoiceListResponseDto>> GetInvoiceList(InvoiceListRequestDto invoiceListRequestDto)
        {
            this.logger.Debug(LogClass.General, $"GetInvoiceList");
            var request = new InvoiceListRetrieveRequest() { OrganizationId = this.OrganizationId };
            var result = await this.invoiceListRetrieveRepository.InvoiceListRetrieveAsync(request);

            return this.Ok(
                new InvoiceListResponseDto
                {
                    IsSuccessful = result.IsSuccessful,
                    InvoiceList = result.InvoiceList
                });
        }
    }
}