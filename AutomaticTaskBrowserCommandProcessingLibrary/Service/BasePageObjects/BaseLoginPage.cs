namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public abstract class BaseLoginPage : BasePage, ILoginPage
    {
        private readonly LoginPageInformation loginPageInformation;

        protected BaseLoginPage(IWebDriver driver, LoginPageInformation loginPageInformation)
            : base(driver)
        {
            this.loginPageInformation = loginPageInformation;
        }

        bool ILoginPage.Submit()
        {
            try
            {
                return this.submit(this.driver, this.loginPageInformation);
            }
            catch
            {
            }

            return false;
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

        protected abstract bool submit(IWebDriver driver, LoginPageInformation loginPageInformation);

        protected abstract bool verifyPageLoaded(IWebDriver driver);

        protected abstract bool verifyPageUrl(IWebDriver driver);
    }
}