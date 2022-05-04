namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public abstract class BaseLoginPage : ILoginPage
    {
        private readonly IWebDriver driver;

        private readonly LoginPageInformation loginPageInformation;

        protected BaseLoginPage(IWebDriver driver, LoginPageInformation loginPageInformation)
        {
            this.driver = driver;
            this.loginPageInformation = loginPageInformation;
        }

        IManagementPage? ILoginPage.Submit()
        {
            try
            {
                return this.submit(this.driver, this.loginPageInformation);
            }
            catch
            {
            }

            return null;
        }

        bool ILoginPage.VerifyPageLoaded()
        {
            try
            {
                return this.verifyPageLoaded(this.driver);
            }
            catch
            {
            }

            return false;
        }

        bool ILoginPage.VerifyPageUrl()
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

        protected abstract IManagementPage? submit(IWebDriver driver, LoginPageInformation loginPageInformation);

        protected abstract bool verifyPageLoaded(IWebDriver driver);

        protected abstract bool verifyPageUrl(IWebDriver driver);
    }
}