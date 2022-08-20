namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementLocateDepositBtn : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain;

        public DistributorToResellerSendPointsTransferManagementLocateDepositBtn(IDistributorToResellerSendPointsTransferChain distributorToResellerSendPointsTransferChain)
        {
            this.distributorToResellerSendPointsTransferChain = distributorToResellerSendPointsTransferChain;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            var response = this.distributorToResellerSendPointsTransferChain.Execute(driver, distributorToResellerSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = DistributorToOperatorTransferResponseType.managementMakeLocateAndClickDepositButton;
            if (response.ManagementPage != null)
            {
                response.IsSuccessful = response.ManagementPage.LocateDepositButtonAndClick(distributorToResellerSendPointsTransferRequest.DestinationAccountId);
            }

            return response;
        }
    }
}
