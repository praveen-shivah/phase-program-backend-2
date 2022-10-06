namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public abstract class BaseManagementPage : BasePage,
                                               IManagementPage
    {
        protected BaseManagementPage(IWebDriver driver)
            : base(driver)
        {
        }

        string IManagementPage.GetBalance()
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

        bool IManagementPage.IsPageUrlSet()
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

        bool IManagementPage.LocateDepositButtonAndClick(string userId)
        {
            try
            {
                return this.locateDepositButtonAndClick(userId);
            }
            catch
            {
            }

            return false;
        }

        bool IManagementPage.MakeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            try
            {
                return this.makeDeposit(amountAsPennies, invoiceLineItemId);
            }
            catch
            {
            }

            return true;
        }

        bool IManagementPage.VerifyFundsAvailable(int points)
        {
            try
            {
                return this.verifyFundsAvailable(points);
            }
            catch
            {
            }

            return false;
        }

        bool IManagementPage.VerifyPageLoaded()
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

        protected abstract bool locateDepositButtonAndClick(string userId);

        protected abstract bool makeDeposit(int amountAsPennies, string invoiceLineItemId);

        protected abstract bool verifyFundsAvailable(int points);

        protected abstract bool verifyPageLoaded();
    }
}