namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    using SeleniumExtras.PageObjects;

    public class RiverSweepsShopsManagement : BaseManagementPage
    {
        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://river-pay.com/agent/show";

        private By currentBalanceAmountLocator = By.XPath("/html/body/div[1]/div[2]/div/div[1]/div[1]/p/b");
        private By depositAmountLocator = By.XPath("//*[@id='modal-deposite-amount']");
        private By tableAccountsLocator = By.Id("table-accounts");

        public RiverSweepsShopsManagement(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        protected override string getBalance()
        {
            var currentBalanceAmount = this.getElementByLocator(this.currentBalanceAmountLocator);
            return currentBalanceAmount.Text.Replace(" ", string.Empty).Replace("usd", string.Empty);
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
        {
            var tableAccounts = this.getElementByLocator(this.tableAccountsLocator);
            var rows = tableAccounts.FindElements(By.TagName("tr"));
            var row = rows.Skip(1).SingleOrDefault(x => x.FindElements(By.TagName("td"))[0].Text == userId);
            if (row == null)
            {
                return false;
            }

            var depositButtonElement = row.FindElements(By.TagName("td"))[5];
            if (depositButtonElement == null)
            {
                return false;
            }

            depositButtonElement.Click();
            var depositAmountElement = this.getElementByLocator(this.depositAmountLocator);

            return true;
        }

        protected override bool makeDeposit(int amount, string invoiceLineItemId)
        {
            var depositAmountElement = this.getElementByLocator(this.depositAmountLocator);
            depositAmountElement.SendKeys(amount.ToString());
            // depositButtonElement.Click();

            return true;
        }

        protected override bool verifyFundsAvailable(int points)
        {
            var currentBalanceAmount = this.getElementByLocator(this.currentBalanceAmountLocator);
            var balanceAsString = currentBalanceAmount.Text.Replace(" ", string.Empty).Replace("usd", string.Empty);
            var currentBalance = decimal.Parse(balanceAsString);
            var pointsAsDollars = points * 1.0M / 100.0M;
            return currentBalance >= pointsAsDollars;
        }

        protected override bool verifyPageLoaded()
        {
            var result = this.wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}