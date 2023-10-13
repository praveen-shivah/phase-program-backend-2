namespace AutomaticTaskBrowserCommandProcessingLibrary.Service.BasePageObjects
{
    using ApiDTO;
    using OpenQA.Selenium;
    using PlayersRepositoryTypes;

    public abstract class BasePlayersPage : BasePage, IResellerPlayerPage
    {
        protected BasePlayersPage(IWebDriver driver)
            : base(driver)
        {
        }
        public bool IsPageUrlSet()
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

        public bool VerifyPageLoaded()
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
        protected abstract ResellerPlayersDetail[] savePlayersDetails(ResellerPlayersRetrieveRequest request);
        protected abstract bool isPageUrlSet();
        protected abstract bool verifyPageLoaded();

        public ResellerPlayersDetail[] SavePlayersDetails(ResellerPlayersRetrieveRequest request)
        {
            try
            {
               
                return this.savePlayersDetails(request);
            }
            catch
            {
            }

            return null;
        }
    }
}
