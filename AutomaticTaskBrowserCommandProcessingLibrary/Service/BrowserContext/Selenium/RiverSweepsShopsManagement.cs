namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    
    using SeleniumExtras.PageObjects;

    public class RiverSweepsShopsManagement
    {
        private readonly IWebDriver driver;

        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://river-pay.com/agent/show";

        [FindsBy(How = How.XPath, Using = "//*[@id='modal-deposite-amount']")]
        [CacheLookup]
        private IWebElement depositAmountElement;

        [FindsBy(How = How.XPath, Using = @"//*[@id='table-accounts']/tbody/tr[8]/td[1]")]
        [CacheLookup]
        private IWebElement jung;

        [FindsBy(How = How.Id, Using = "table-accounts")]
        [CacheLookup]
        private IWebElement tableAccounts;

        public RiverSweepsShopsManagement(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public bool IsPageUrlSet()
        {
            try
            {
                var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
                var result = wait.Until(d => d.Url.Contains(this.pageUrl));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
            }

            return false;
        }

        public bool MakeDeposit(string userId, int amount)
        {
            var rows = this.tableAccounts.FindElements(By.TagName("tr"));
            var row = rows.Skip(1).SingleOrDefault(x => x.FindElements(By.TagName("td"))[0].Text == userId);
            if (row == null)
            {
                return false;
            }

            var balanceElement = row.FindElements(By.TagName("td"))[4];
            var startingBalance = balanceElement.Text;
            var depositButtonElement = row.FindElements(By.TagName("td"))[5];
            depositButtonElement.Click();

            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(1)).Until(_ => this.depositAmountElement.Displayed);
            this.depositAmountElement.SendKeys(amount.ToString());

            return true;
        }

        /// <summary>
        ///     Verify that the page loaded completely.
        /// </summary>
        /// <returns>The Login class instance.</returns>
        public bool VerifyPageLoaded()
        {
            try
            {
                var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
                var result = wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
                return result;
            }
            catch (WebDriverTimeoutException)
            {
            }

            return false;
        }
    }
}