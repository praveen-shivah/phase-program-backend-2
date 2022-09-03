namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using OpenQA.Selenium;

    public interface ILogoutPageFactory
    {
        ILogoutPage Create(IWebDriver webDriver, SoftwareTypeEnum softwareType);
    }
}