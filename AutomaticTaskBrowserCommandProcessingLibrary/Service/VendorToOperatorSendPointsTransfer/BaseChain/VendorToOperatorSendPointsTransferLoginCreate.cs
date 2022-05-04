namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferLoginCreate : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer;

        private readonly IVendorToOperatorTransferLoginPageFactory vendorToOperatorTransferLoginPageFactory;

        public VendorToOperatorSendPointsTransferLoginCreate(IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer, IVendorToOperatorTransferLoginPageFactory vendorToOperatorTransferLoginPageFactory)
        {
            this.riverSweepsVendorToOperatorSendPointsTransfer = riverSweepsVendorToOperatorSendPointsTransfer;
            this.vendorToOperatorTransferLoginPageFactory = vendorToOperatorTransferLoginPageFactory;
        }

        VendorToOperatorTransferResponse IVendorToOperatorSendPointsTransferChain.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.riverSweepsVendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.VendorToOperatorTransferResponseType = VendorToOperatorTransferResponseType.loginCreate;

            try
            {
                response.LoginPage = this.vendorToOperatorTransferLoginPageFactory.Create(driver, vendorToOperatorSendPointsTransferRequest);
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
