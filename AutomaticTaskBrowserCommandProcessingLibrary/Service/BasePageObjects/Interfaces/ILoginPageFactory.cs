namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public interface ILoginPageFactory
    {
        ILoginPage Create(
            IWebDriver webDriver,
            LoginPageInformation loginPageInformation);
    }
}