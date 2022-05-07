namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public abstract class BaseResellerBalancePage : IResellerBalancePage
    {
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