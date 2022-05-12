namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium;

    public interface IResellerBalancePageFactory
    {
        IResellerBalancePage Create(IWebDriver driver, SoftwareType softwareType);
    }
}
