namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class RiverSweepsVendorToOperatorSendPointsTransferLoginCreate : IRiverSweepsVendorToOperatorSendPointsTransfer
    {
        private readonly IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer;

        public RiverSweepsVendorToOperatorSendPointsTransferLoginCreate(IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer)
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

            response.RiverSweepsVendorToOperatorTransferResponseType = RiverSweepsVendorToOperatorTransferResponseType.loginCreate;

            try
            {
                response.Login = new RiverSweepsVendorToOperatorTransferLogin(driver, vendorToOperatorSendPointsTransferRequest);
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
