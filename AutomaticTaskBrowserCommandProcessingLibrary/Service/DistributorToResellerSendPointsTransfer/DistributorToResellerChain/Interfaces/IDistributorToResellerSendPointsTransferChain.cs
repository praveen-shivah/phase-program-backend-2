namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IDistributorToResellerSendPointsTransferChain
    {
        DistributorToResellerTransferResponse Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest);
    }
}
