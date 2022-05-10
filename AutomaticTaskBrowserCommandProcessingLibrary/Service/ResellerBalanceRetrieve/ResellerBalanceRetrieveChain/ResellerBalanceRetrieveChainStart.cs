namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class ResellerBalanceRetrieveChainStart : IResellerBalanceRetrieveChain
    {
        ResellerBalanceRetrieveResponse IResellerBalanceRetrieveChain.Execute(
            IWebDriver driver,
            ResellerBalanceRetrieveRequest resellerBalanceRetrieveRequest)
        {
            return new ResellerBalanceRetrieveResponse
            {
                IsSuccessful = true,
                ResponseType = VendorBalanceRetrieveResponseType.start
            };
        }
    }
}