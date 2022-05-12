namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium;

    public interface IManagementPageFactory
    {
        IManagementPage Create(IWebDriver webDriver, SoftwareType softwareType);
    }
}
