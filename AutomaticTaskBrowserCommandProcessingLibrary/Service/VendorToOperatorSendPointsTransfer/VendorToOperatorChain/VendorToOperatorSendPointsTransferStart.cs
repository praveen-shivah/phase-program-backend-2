namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferStart : IVendorToOperatorSendPointsTransferChain
    {
        VendorToOperatorTransferResponse IVendorToOperatorSendPointsTransferChain.Execute(
            IWebDriver driver,
            VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            return new VendorToOperatorTransferResponse
            {
                IsSuccessful = true,
                ResponseType = VendorToOperatorTransferResponseType.start
            };
        }
    }
}