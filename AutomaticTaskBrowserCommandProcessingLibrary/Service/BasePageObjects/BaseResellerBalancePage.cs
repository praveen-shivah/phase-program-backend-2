namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public abstract class BaseResellerBalancePage : BasePage, IResellerBalancePage
    {
        protected BaseResellerBalancePage(IWebDriver driver)
            : base(driver)
        {
        }

        string IResellerBalancePage.GetBalance()
        {
            try
            {
                return this.getBalance();
            }
            catch
            {
            }

            return string.Empty;
        }

        bool IResellerBalancePage.IsPageUrlSet()
        {
            try
            {
                return this.isPageUrlSet();
            }
            catch
            {
            }

            return false;
        }

        bool IResellerBalancePage.VerifyPageLoaded()
        {
            try
            {
                return this.verifyPageLoaded();
            }
            catch
            {
            }

            return false;
        }

        protected abstract string getBalance();

        protected abstract bool isPageUrlSet();

        protected abstract bool verifyPageLoaded();
    }
}