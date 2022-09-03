namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class UltraMonsterShopsManagement : BaseManagementPage
    {
        private readonly IWebDriver driver;

        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://go123.ultramonster.net/#/manage-user/account";

        [FindsBy(How = How.XPath, Using = @"/html/body/div[1]/div/div[2]/div/div[1]/div[3]/p/span")]
        [CacheLookup]
        private IWebElement? currentBalanceAmount;

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div/div[2]/div/div[2]/div/div[1]/div/span[4]")]
        [CacheLookup]
        private IWebElement searchButtonElement;

        [FindsBy(How = How.Id, Using = "table-accounts")]
        [CacheLookup]
        private IWebElement tableAccounts;

        public UltraMonsterShopsManagement(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        protected override string getBalance()
        {
            if (this.currentBalanceAmount == null)
            {
                return "0.00";
            }

            return this.currentBalanceAmount.Text;
        }

        protected override bool isPageUrlSet()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
        {
            this.searchButtonElement.Click();

            // Find 
            var searchUserButton = this.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/section/div/div[1]/form/div[2]/div/div/div/div/input"));
            if (searchUserButton == null)
            {
                return false;
            }

            var searchInputBoxElement = this.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[2]/div/a/span"));
            if (searchInputBoxElement == null)
            {
                return false;
            }

            searchInputBoxElement.SendKeys(userId);

            var okButton = this.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/section/div/div[1]/form/div[3]/div/button"));
            if (okButton == null)
            {
                return false;
            }

            okButton.Click();

            var userAccountIdElement = this.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[2]/div/a/span"));
            if (userAccountIdElement == null)
            {
                return false;
            }

            if (userAccountIdElement.Text.ToLower() != userId.ToLower())
            {
                return false;
            }

            var setScoreButton = this.driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[10]/div/button[1]/span"));
            if (setScoreButton == null)
            {
                return false;
            }

            setScoreButton.Click();

            return true;
        }

        protected override bool makeDeposit(int amount)
        {
            // this.depositAmountElement.SendKeys(amount.ToString());
            // this.depositButtonElement.Click();

            return true;
        }

        protected override bool verifyFundsAvailable(int points)
        {
            if (this.currentBalanceAmount == null)
            {
                return false;
            }

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