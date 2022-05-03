namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class RiverSweepsVendorToOperatorSendPointsTransferLoginVerifyLoad : IRiverSweepsVendorToOperatorSendPointsTransfer
    {
        private readonly IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer;

        public RiverSweepsVendorToOperatorSendPointsTransferLoginVerifyLoad(IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer)
        {
            this.riverSweepsVendorToOperatorSendPointsTransfer = riverSweepsVendorToOperatorSendPointsTransfer;
        }

        RiverSweepsVendorToOperatorTransferResponse IRiverSweepsVendorToOperatorSendPointsTransfer.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.riverSweepsVendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.RiverSweepsVendorToOperatorTransferResponseType = RiverSweepsVendorToOperatorTransferResponseType.loginVerifyLoad;
            response.Login.VerifyPageLoaded();

            return response;
        }
    }
}
