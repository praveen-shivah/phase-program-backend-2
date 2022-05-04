namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium;

    public class VendorToOperatorTransferLoginPageFactory : IVendorToOperatorTransferLoginPageFactory
    {
        IVendorToOperatorTransferLoginPage IVendorToOperatorTransferLoginPageFactory.Create(IWebDriver webDriver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            switch (vendorToOperatorSendPointsTransferRequest.SoftwareType)
            {
                case SoftwareType.riverSweeps:
                    return new RiverSweepsVendorToOperatorTransferLogin(webDriver, vendorToOperatorSendPointsTransferRequest);
                default:
                    throw new ArgumentOutOfRangeException(nameof(vendorToOperatorSendPointsTransferRequest.SoftwareType), vendorToOperatorSendPointsTransferRequest.SoftwareType, null);
            }
        }
    }
}