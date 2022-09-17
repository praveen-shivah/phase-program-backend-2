namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using ApiRequestLibrary;

    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferUpdateApi : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        private readonly IResellerTransferPointsCompleted resellerTransferPointsCompleted;

        public DistributorToResellerSendPointsTransferUpdateApi(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain, IResellerTransferPointsCompleted resellerTransferPointsCompleted)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
            this.resellerTransferPointsCompleted = resellerTransferPointsCompleted;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(
            IWebDriver driver,
            DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var markAsCompleted = new ResellerTransferPointsCompletedDto()
            {
                IsSuccessful = true,
                InvoiceLineItemId = distributorToResellerSendPointsTransferRequest.InvoiceLineItemId
            };

            this.resellerTransferPointsCompleted.MarkAsCompleted(markAsCompleted);

            return response;
        }
    }
}
