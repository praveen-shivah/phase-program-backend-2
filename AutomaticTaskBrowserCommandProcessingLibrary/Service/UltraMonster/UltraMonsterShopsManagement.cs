namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class UltraMonsterShopsManagement : BaseManagementPage
    {
        private readonly IWebDriver driver;

        private readonly string pageLoadedText = "Agent account";

        private readonly string pageUrl = "https://go123.ultramonster.net/#/manage-user/search";

        private By currentBalanceAmountElementLocator = By.XPath("//*[@id='app']/div/div[2]/div/div[1]/div[3]/p");
        private By setPointsInputELementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[4]/div/form/div[3]/div/div/input");
        private By setPointsRemarksInputELementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[4]/div/form/div[4]/div/div/textarea");
        private By setScoreButtonLocator = By.XPath("/html/body/div[1]/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[10]/div/button[1]/span");
        private By okButtonLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[1]/form/div[3]/div/button");
        private By searchInputBoxElementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[1]/form/div[2]/div/div/div/div[1]/input");
        private By userAccountIdElementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[2]/div/a/span");
        private By agentAccountRadioButtonElementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[1]/form/div[1]/div/div/label[1]/span[2]");

        public UltraMonsterShopsManagement(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
        }

        protected override string getBalance()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var currentBalanceAmountElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.currentBalanceAmountElementLocator));
            if (currentBalanceAmountElement == null)
            {
                return "0.00";
            }

            var balanceAsString = currentBalanceAmountElement.Text.Replace(" ", string.Empty).Replace("Myscore:", string.Empty);
            return balanceAsString;
        }

        protected override bool isPageUrlSet()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));

            var agentAccountRadioButtonElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.agentAccountRadioButtonElementLocator));
            if (agentAccountRadioButtonElement == null)
            {
                return false;
            }

            agentAccountRadioButtonElement.Click();

            var searchInputBoxElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.searchInputBoxElementLocator));
            if (searchInputBoxElement == null)
            {
                return false;
            }

            searchInputBoxElement.SendKeys(userId);

            var okButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.okButtonLocator));
            if (okButton == null)
            {
                return false;
            }

            okButton.Click();

            var userAccountIdElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(userAccountIdElementLocator));
            if (userAccountIdElement == null)
            {
                return false;
            }

            var test = userAccountIdElement.Text.ToLower();
            if (test != userId.ToLower())
            {
                return false;
            }

            var setScoreButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.setScoreButtonLocator));
            if (setScoreButton == null)
            {
                return false;
            }

            setScoreButton.Click();

            return true;
        }

        protected override bool makeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var setPointsInputELement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.setPointsInputELementLocator));
            if (setPointsInputELement == null)
            {
                return false;
            }

            var amountAsPenniesAsDollars = Math.Round(amountAsPennies / 100.0, 2);
            setPointsInputELement.SendKeys(amountAsPenniesAsDollars.ToString());

            var setPointsRemarksInputELement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.setPointsRemarksInputELementLocator));
            if (setPointsRemarksInputELement == null)
            {
                return false;
            }

            setPointsRemarksInputELement.SendKeys($"Invoice line item id: {invoiceLineItemId}");

            // this.depositButtonElement.Click();

            return true;
        }

        protected override bool verifyFundsAvailable(int points)
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var currentBalanceAmountElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(this.currentBalanceAmountElementLocator));
            if (currentBalanceAmountElement == null)
            {
                return false;
            }

            var balanceAsString = currentBalanceAmountElement.Text.Replace(" ", string.Empty).Replace("Myscore:", string.Empty);
            var currentBalance = decimal.Parse(balanceAsString);
            var pointsAsDollars = points * 1.0M / 100.0M;
            return currentBalance >= pointsAsDollars;
        }

        protected override bool verifyPageLoaded()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => this.checkPageSource(d.PageSource));
            return result;
        }

        private bool checkPageSource(string pageSource)
        {
            return pageSource.Contains(this.pageLoadedText);
        }
    }
}