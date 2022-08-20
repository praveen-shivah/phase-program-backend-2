namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementMakeDeposit : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        public DistributorToResellerSendPointsTransferManagementMakeDeposit(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful || response.ManagementPage == null)
            {
                return response;
            }

            response.ResponseType = DistributorToOperatorTransferResponseType.managementMakeDeposit;

            // We'll consider it successful if we get this far so as not to duplicate deposits.
            // Any failure up to this point and we can do a retry.
            response.ManagementPage.MakeDeposit(distributorToResellerSendPointsTransferRequest.Points);

            return response;
        }
    }
}
