namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class RiverSweepsVendorToOperatorSendPointsTransferLogin : IRiverSweepsVendorToOperatorSendPointsTransfer
    {
        private readonly IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer;

        public RiverSweepsVendorToOperatorSendPointsTransferLogin(IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer)
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

            response.RiverSweepsVendorToOperatorTransferResponseType = RiverSweepsVendorToOperatorTransferResponseType.loginSubmit;

            response.Management = response.Login.Submit();
            if (response.Management == null)
            {
                response.IsSuccessful = false;
            }


            return response;
        }
    }
}
