namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorBalanceRetrieveAdapter : IVendorBalanceRetrieveAdapter
    {
        private readonly IVendorBalanceRetrieveChain vendorBalanceRetrieveChain;

        public VendorBalanceRetrieveAdapter(IVendorBalanceRetrieveChain vendorBalanceRetrieveChain)
        {
            this.vendorBalanceRetrieveChain = vendorBalanceRetrieveChain;
        }

        VendorBalanceRetrieveResponse IVendorBalanceRetrieveAdapter.Execute(IWebDriver driver, VendorBalanceRetrieveRequest vendorBalanceRetrieveRequest)
        {
            return this.vendorBalanceRetrieveChain.Execute(driver, vendorBalanceRetrieveRequest);
        }
    }
}
