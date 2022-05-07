namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium;

    public interface IResellerBalancePageFactory
    {
        IResellerBalancePage Create(IWebDriver driver, SoftwareType softwareType);
    }
}
