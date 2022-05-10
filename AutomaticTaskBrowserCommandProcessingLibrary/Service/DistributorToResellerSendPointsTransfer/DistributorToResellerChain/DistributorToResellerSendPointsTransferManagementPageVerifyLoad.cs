namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferManagementPageVerifyLoad : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransfer;

        public DistributorToResellerSendPointsTransferManagementPageVerifyLoad(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransfer)
        {
            this.vendorToOperatorSendPointsTransfer = vendorToOperatorSendPointsTransfer;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.managementVerifyLoad;
            if (response.ManagementPage.IsPageUrlSet() && response.ManagementPage.VerifyPageLoaded())
            {
                return response;
            }

            response.IsSuccessful = false;

            return response;
        }
    }
}
