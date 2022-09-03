namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public abstract class BaseLogoutPage : ILogoutPage
    {
        private readonly IWebDriver driver;

        protected BaseLogoutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        bool ILogoutPage.Logout()
        {
            try
            {
                return this.logout(this.driver);
            }
            catch
            {
            }

            return false;
        }

        bool ILogoutPage.VerifyPageUrl()
        {
            try
            {
                return this.verifyPageUrl(this.driver);
            }
            catch
            {
            }

            return false;
        }

        protected abstract bool verifyPageUrl(IWebDriver webDriver);

        protected abstract bool logout(IWebDriver webDriver);
    }
}