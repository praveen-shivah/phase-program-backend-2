namespace ApiHost
{
    using System.Threading.Tasks;

    using AutomaticTaskBrowserCommandProcessingLibrary;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IDistributorToResellerSendPointsTransferProcessor distributorToResellerSendPointsTransferProcessor;

        private readonly ILogger logger;

        private readonly IResellerBalanceRetrieveProcessor resellerBalanceRetrieveProcessor;

        private readonly IResellerTransactionRetrieveProcessor resellerTransactionRetrieveProcessor;

        public MessageController(
            ILogger logger,
            IDistributorToResellerSendPointsTransferProcessor distributorToResellerSendPointsTransferProcessor,
            IResellerBalanceRetrieveProcessor resellerBalanceRetrieveProcessor,
            IResellerTransactionRetrieveProcessor resellerTransactionRetrieveProcessor)
        {
            this.logger = logger;
            this.distributorToResellerSendPointsTransferProcessor = distributorToResellerSendPointsTransferProcessor;
            this.resellerBalanceRetrieveProcessor = resellerBalanceRetrieveProcessor;
            this.resellerTransactionRetrieveProcessor = resellerTransactionRetrieveProcessor;
        }

        [HttpPost("retrieve-balance")]
        [AllowAnonymous]
        public async Task<ActionResult<ResellerBalanceRetrieveResponseDto>> RetrieveBalance(ResellerBalanceRetrieveRequestDto request)
        {
            this.logger.Debug(LogClass.General, "RetrieveBalance received");
            var response = await this.resellerBalanceRetrieveProcessor.Execute(request);
            var result = new ResellerBalanceRetrieveResponseDto
                             {
                                 IsSuccessful = response.IsSuccessful,
                                 BalanceAsPoints = response.BalanceAsPoints
                             };
            return this.Ok(result);
        }

        [HttpPost("transfer-points")]
        [AllowAnonymous]
        public async Task<ActionResult<DistributorToOperatorSendPointsTransferResponseDto>> TransferPoints(DistributorToResellerSendPointsTransferRequestDto request)
        {
            this.logger.Debug(LogClass.General, "TransferPoints received");
            var response = await this.distributorToResellerSendPointsTransferProcessor.Execute(request);
            var result = new DistributorToOperatorSendPointsTransferResponseDto { IsSuccessful = response };
            return this.Ok(result);
        }

        [HttpPost("retrieve-transaction")]
        [AllowAnonymous]
        public async Task<ActionResult<ResellerTransactionRetrieveResponseDto>> RetrieveTransaction(ResellerTransactionRetrieveRequestDto request)
        {
            this.logger.Debug(LogClass.General, "RetrieveBalance received");
            var response = await this.resellerTransactionRetrieveProcessor.Execute(request);
            var result = new ResellerTransactionRetrieveResponseDto
            {
                IsSuccessful = response.IsSuccessful,
                Details = response.Details
            };
            return this.Ok(result);
        }
    }
}