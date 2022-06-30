namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium;

    public class ResellerBalancePageFactory : IResellerBalancePageFactory
    {
        IResellerBalancePage IResellerBalancePageFactory.Create(IWebDriver driver, SoftwareType softwareType)
        {
            switch (softwareType)
            {
                case SoftwareType.riverSweeps:
                    return new RiverSweepsRellerBalancePage(driver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
