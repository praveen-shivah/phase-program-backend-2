namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium;

    public class ResellerBalancePageFactory : IResellerBalancePageFactory
    {
        IResellerBalancePage IResellerBalancePageFactory.Create(IWebDriver driver, SoftwareTypeEnum softwareType)
        {
            switch (softwareType)
            {
                case SoftwareTypeEnum.riverSweeps:
                    return new RiverSweepsRellerBalancePage(driver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
