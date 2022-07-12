namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using OpenQA.Selenium;

    public interface IResellerBalancePageFactory
    {
        IResellerBalancePage Create(IWebDriver driver, SoftwareTypeEnum softwareType);
    }
}
