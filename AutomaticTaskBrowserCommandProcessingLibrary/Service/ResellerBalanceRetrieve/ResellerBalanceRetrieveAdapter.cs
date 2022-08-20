namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveAdapter : IResellerBalanceRetrieveAdapter
    {
        private readonly IResellerBalanceRetrieveChain resellerBalanceRetrieveChain;

        public ResellerBalanceRetrieveAdapter(IResellerBalanceRetrieveChain resellerBalanceRetrieveChain)
        {
            this.resellerBalanceRetrieveChain = resellerBalanceRetrieveChain;
        }

        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveAdapter.Execute(IWebDriver driver, ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            return this.resellerBalanceRetrieveChain.Execute(driver, resellerBalanceRetrieveRequest);
        }
    }
}
