namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementLocateDepositBtn : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public DistributorToResellerSendPointsTransferManagementLocateDepositBtn(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransferChain.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.managementMakeLocateAndClickDepositButton;
            if (response.ManagementPage != null)
            {
                response.IsSuccessful = response.ManagementPage.LocateDepositButtonAndClick(vendorToOperatorSendPointsTransferRequest.DestinationAccountId);
            }

            return response;
        }
    }
}
