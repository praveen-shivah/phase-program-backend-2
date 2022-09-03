namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferStart : IDistributorToResellerSendPointsTransferChain
    {
        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(
            IWebDriver driver,
            DistributorToResellerSendPointsTransferRequest distributorToResellerSendPointsTransferRequest)
        {
            return new DistributorToResellerTransferResponse
            {
                IsSuccessful = true,
                ResponseType = DistributorToOperatorTransferResponseType.start
            };
        }
    }
}