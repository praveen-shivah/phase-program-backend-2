namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferManagementLocateDepositBtn : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public VendorToOperatorSendPointsTransferManagementLocateDepositBtn(IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
        }

        VendorToOperatorTransferResponse IVendorToOperatorSendPointsTransferChain.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
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
