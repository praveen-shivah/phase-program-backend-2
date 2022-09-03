namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface IDistributorToResellerSendPointsTransferAdapter
    {
        DistributorToResellerTransferResponse Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest);
    }
}
