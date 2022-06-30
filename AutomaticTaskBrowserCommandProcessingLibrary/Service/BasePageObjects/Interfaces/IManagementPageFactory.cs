namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using OpenQA.Selenium;

    public interface IManagementPageFactory
    {
        IManagementPage Create(IWebDriver webDriver, SoftwareType softwareType);
    }
}
