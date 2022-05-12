namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveAdapter : IResellerBalanceRetrieveAdapter
    {
        private readonly IResellerBalanceRetrieveChain vendorBalanceRetrieveChain;

        public ResellerBalanceRetrieveAdapter(IResellerBalanceRetrieveChain vendorBalanceRetrieveChain)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveAdapter.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            return this.vendorBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
        }
    }
}
