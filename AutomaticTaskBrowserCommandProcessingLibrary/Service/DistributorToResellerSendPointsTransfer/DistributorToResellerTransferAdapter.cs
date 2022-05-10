namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerTransferAdapter : IDistributorToResellerSendPointsTransferAdapter
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain;

        public DistributorToResellerTransferAdapter(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransferChain)
        {
            this.vendorToOperatorSendPointsTransferChain = vendorToOperatorSendPointsTransferChain;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferAdapter.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            return this.vendorToOperatorSendPointsTransferChain.Execute(driver, vendorToOperatorSendPointsTransferRequest);
        }
    }
}
