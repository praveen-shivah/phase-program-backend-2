using Microsoft.AspNetCore.Mvc;

namespace ApiHost
{
    using System.Threading.Tasks;

    using ApiDTO;

    using AutomaticTaskBrowserCommandProcessingLibrary;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly ILogger logger;

        private readonly IDistributorToResellerSendPointsTransferProcessor distributorToResellerSendPointsTransferProcessor;

        private readonly IResellerBalanceRetrieveProcessor resellerBalanceRetrieveProcessor;

        public MessageController(ILogger logger, IDistributorToResellerSendPointsTransferProcessor distributorToResellerSendPointsTransferProcessor,
                                 IResellerBalanceRetrieveProcessor resellerBalanceRetrieveProcessor)
        {
            this.logger = logger;
            this.distributorToResellerSendPointsTransferProcessor = distributorToResellerSendPointsTransferProcessor;
            this.resellerBalanceRetrieveProcessor = resellerBalanceRetrieveProcessor;
        }

        [HttpPost("transfer-points")]
        [AllowAnonymous]
        public async Task<ActionResult<DistributorToOperatorSendPointsTransferResponseDto>> TransferPoints(DistributorToResellerSendPointsTransferRequestDto request)
        {
            this.logger.Debug(LogClass.General, "TransferPoints received");
            var response = await this.distributorToResellerSendPointsTransferProcessor.Execute(request);
            var result = new DistributorToOperatorSendPointsTransferResponseDto() { IsSuccessful = response };
            return this.Ok(result);
        }

        [HttpPost("retrieve-balance")]
        [AllowAnonymous]
        public async Task<ActionResult<ResellerBalanceRetrieveResponseDto>> RetrieveBalance(ResellerBalanceRetrieveRequestDto request)
        {
            this.logger.Debug(LogClass.General, "RetrieveBalance received");
            var response = await this.resellerBalanceRetrieveProcessor.Execute(request);
            var result = new ResellerBalanceRetrieveResponseDto() { IsSuccessful = response };
            return this.Ok(result);
        }
    }
}
