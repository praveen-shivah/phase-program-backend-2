namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementVerifyFundsAvailable : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        public DistributorToResellerSendPointsTransferManagementVerifyFundsAvailable(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain)
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

            response.ResponseType = DistributorToOperatorTransferResponseType.managementVerifyFundsAvailable;
            response.IsSuccessful = response.ManagementPage.VerifyFundsAvailable(distributorToResellerSendPointsTransferRequest.Points);

            return response;
        }
    }
}
