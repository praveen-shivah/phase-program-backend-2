namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;
    using OpenQA.Selenium;
    public interface IResellerPlayerPageFactory
    {
        IResellerPlayerPage Create(IWebDriver webDriver, SoftwareTypeEnum softwareType);
    }
}
