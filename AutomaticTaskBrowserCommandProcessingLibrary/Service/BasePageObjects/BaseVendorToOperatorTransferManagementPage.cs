namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public abstract class BaseVendorToOperatorTransferManagementPage : IManagementPage
    {
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

        protected abstract string getBalance();

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

        bool IManagementPage.MakeDeposit(int amount)
        {
            try
            {
                return this.makeDeposit(amount);
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

        protected abstract bool verifyPageLoaded();

        protected abstract bool isPageUrlSet();

        protected abstract bool locateDepositButtonAndClick(string userId);

        protected abstract bool makeDeposit(int amount);

        protected abstract bool verifyFundsAvailable(int points);
    }
}