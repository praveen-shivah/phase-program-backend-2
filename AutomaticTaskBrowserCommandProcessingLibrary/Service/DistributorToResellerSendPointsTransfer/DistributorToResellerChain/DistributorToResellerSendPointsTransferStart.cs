namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferStart : IDistributorToResellerSendPointsTransferChain
    {
        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(
            IWebDriver driver,
            DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            return new DistributorToResellerTransferResponse
            {
                IsSuccessful = true,
                ResponseType = VendorToOperatorTransferResponseType.start
            };
        }
    }
}