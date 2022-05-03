namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class RiverSweepsVendorToOperatorTransferShopsManagement
    {
        private readonly IWebDriver driver;

        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://river-pay.com/agent/show";

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[2]/div/div[1]/div[1]/p/b")]
        [CacheLookup]
        private IWebElement currentBalanceAmount;

        [FindsBy(How = How.XPath, Using = "//*[@id='modal-deposite-amount']")]
        [CacheLookup]
        private IWebElement depositAmountElement;

        [FindsBy(How = How.XPath, Using = "//*[@id='yw0']/div[3]/input[1]")]
        [CacheLookup]
        private IWebElement depositBtnElement;

        private IWebElement depositButtonElement;

        [FindsBy(How = How.XPath, Using = @"//*[@id='table-accounts']/tbody/tr[8]/td[1]")]
        [CacheLookup]
        private IWebElement jung;
        
        [FindsBy(How = How.Id, Using = "table-accounts")]
        [CacheLookup]
        private IWebElement tableAccounts;

        public RiverSweepsVendorToOperatorTransferShopsManagement(IWebDriver driver)
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

        public bool LocateDepositButtonAndClick(string userId)
        {
            var rows = this.tableAccounts.FindElements(By.TagName("tr"));
            var row = rows.Skip(1).SingleOrDefault(x => x.FindElements(By.TagName("td"))[0].Text == userId);
            if (row == null)
            {
                return false;
            }

            this.depositButtonElement = row.FindElements(By.TagName("td"))[5];
            if (this.depositButtonElement == null)
            {
                return false;
            }

            this.depositButtonElement.Click();
            try
            {
                var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(1)).Until(_ => this.depositAmountElement.Displayed && this.depositButtonElement.Displayed);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool VerifyFundsAvailable(int points)
        {
            var currentBalance = decimal.Parse(this.currentBalanceAmount.Text.Replace(" ", string.Empty));
            var pointsAsDollars = points * 1.0M / 100.0M;
            return currentBalance >= points;
        }

        public bool MakeDeposit(int amount)
        {
            this.depositAmountElement.SendKeys(amount.ToString());
            // this.depositButtonElement.Click();

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