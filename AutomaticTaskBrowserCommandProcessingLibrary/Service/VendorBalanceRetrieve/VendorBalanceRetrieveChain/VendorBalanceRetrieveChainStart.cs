namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorBalanceRetrieveChainStart : IVendorBalanceRetrieveChain
    {
        VendorBalanceRetrieveResponse IVendorBalanceRetrieveChain.Execute(
            IWebDriver driver,
            VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest)
        {
            return new VendorBalanceRetrieveResponse
            {
                IsSuccessful = true,
                ResponseType = VendorBalanceRetrieveResponseType.start
            };
        }
    }
}