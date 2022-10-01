namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class RiverSweepsShopsManagement : BaseManagementPage
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

        private IWebElement depositButtonElement;

        [FindsBy(How = How.Id, Using = "table-accounts")]
        [CacheLookup]
        private IWebElement tableAccounts;

        public RiverSweepsShopsManagement(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        protected override string getBalance()
        {
            return this.currentBalanceAmount.Text.Replace(" ", string.Empty).Replace("usd", string.Empty);
        }

        protected override bool isPageUrlSet()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
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
            var result = new WebDriverWait(this.driver, TimeSpan.FromSeconds(1)).Until(_ => this.depositAmountElement.Displayed && this.depositButtonElement.Displayed);
            return result;
        }

        protected override bool makeDeposit(int amount, string invoiceLineItemId)
        {
            this.depositAmountElement.SendKeys(amount.ToString());
            // this.depositButtonElement.Click();

            return true;
        }

        protected override bool verifyFundsAvailable(int points)
        {
            var balanceAsString = this.currentBalanceAmount.Text.Replace(" ", string.Empty).Replace("usd", string.Empty);
            var currentBalance = decimal.Parse(balanceAsString);
            var pointsAsDollars = points * 1.0M / 100.0M;
            return currentBalance >= pointsAsDollars;
        }

        protected override bool verifyPageLoaded()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}