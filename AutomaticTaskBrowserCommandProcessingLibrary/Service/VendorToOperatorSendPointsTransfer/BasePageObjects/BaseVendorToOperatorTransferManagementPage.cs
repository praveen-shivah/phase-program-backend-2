namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public abstract class BaseVendorToOperatorTransferManagementPage : IVendorToOperatorTransferManagementPage
    {
        bool IVendorToOperatorTransferManagementPage.IsPageUrlSet()
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

        bool IVendorToOperatorTransferManagementPage.LocateDepositButtonAndClick(string userId)
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

        bool IVendorToOperatorTransferManagementPage.MakeDeposit(int amount)
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

        bool IVendorToOperatorTransferManagementPage.VerifyFundsAvailable(int points)
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

        bool IVendorToOperatorTransferManagementPage.VerifyPageLoaded()
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