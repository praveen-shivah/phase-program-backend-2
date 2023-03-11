namespace ApiHost
{
    using ApiDTO;
    using APISupport;
    using AuthenticationRepositoryTypes;

    using InvoiceRepository;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    [AuthorizePolicy]
    [ApiController]
    [Route("api/invoicereports")]
    public class InvoiceReportController : ApiControllerBase
    {
        // private readonly IMessageSession messageSession;

        private readonly ILogger logger;

        private readonly IInvoiceListRetrieveRepository invoiceListRetrieveRepository;

        private readonly IInvoiceListResellerRetrieveRepository invoiceListResellerRetrieveRepository;

        public InvoiceReportController(ILogger logger, 
                                       IInvoiceListRetrieveRepository invoiceListRetrieveRepository,
                                       IInvoiceListResellerRetrieveRepository invoiceListResellerRetrieveRepository)
        {
            // this.messageSession = messageSession;
            this.logger = logger;
            this.invoiceListRetrieveRepository = invoiceListRetrieveRepository;
            this.invoiceListResellerRetrieveRepository = invoiceListResellerRetrieveRepository;
        }

        [HttpPost("get-invoice-list")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
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

        [HttpPost("get-reseller-invoice-list")]
        [AuthorizePolicy(Policy = AuthenticationConstants.POLICY_ALL)]
        public async Task<ActionResult<InvoiceListResellerRetrieveResponseDto>> GetResellerInvoiceList(InvoiceListResellerRetrieveRequestDto invoiceListResellerRetrieveRequestDto)
        {
            this.logger.Debug(LogClass.General, $"GetResellerInvoiceList");
            var request = new InvoiceListResellerRetrieveRequest() { OrganizationId = this.OrganizationId, ResellerId = invoiceListResellerRetrieveRequestDto.ResellerId};
            ;
            var result = await this.invoiceListResellerRetrieveRepository.InvoiceListResellerRetrieveAsync(request);

            return this.Ok(
                new InvoiceListResellerRetrieveResponseDto
                {
                        IsSuccessful = result.IsSuccessful,
                        InvoiceList = result.InvoiceList
                    });
        }
    }
}