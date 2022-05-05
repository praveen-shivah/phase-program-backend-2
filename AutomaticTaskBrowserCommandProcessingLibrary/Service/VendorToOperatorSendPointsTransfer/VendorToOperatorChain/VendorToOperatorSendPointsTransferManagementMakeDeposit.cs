namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferManagementMakeDeposit : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public VendorToOperatorSendPointsTransferManagementMakeDeposit(IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
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

            response.ResponseType = VendorToOperatorTransferResponseType.managementMakeDeposit;

            // We'll consider it successful if we get this far so as not to duplicate deposits.
            // Any failure up to this point and we can do a retry.
            response.ManagementPage.MakeDeposit(vendorToOperatorSendPointsTransferRequest.Points);

            return response;
        }
    }
}
