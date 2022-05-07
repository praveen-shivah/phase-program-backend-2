namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium;

    public interface IManagementPageFactory
    {
        IManagementPage Create(IWebDriver webDriver, SoftwareType softwareType);
    }
}
