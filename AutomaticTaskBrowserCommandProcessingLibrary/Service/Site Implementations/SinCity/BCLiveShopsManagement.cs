namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class BCLiveShopsManagement : BaseManagementPage
    {
        private readonly string pageLoadedText = "Welcome,";

        private readonly string pageUrl = "http://byagent.bclive.vip/Index.aspx?73";

        private readonly By agentManagementBalanceLocator = By.XPath("//*[@id=\"form1\"]/table[1]/tbody/tr/td[2]");
        private readonly By agentManagementButtonLocator = By.XPath("//*[@id=\"M_1\"]/table/tbody/tr[2]");

        private readonly By agentAccountsSearchInputLocator = By.XPath("//*[@id=\"txtAccounts\"]");
        private readonly By agentAccountSearchButtonLocator = By.Id("btnQuery");

        private readonly By rechargeLinkLocator = By.XPath("//*[@id=\"list\"]/tbody/tr[2]/td[2]/a[2]");
        private readonly By redeemLinkLocator = By.XPath("//*[@id=\"list\"]/tbody/tr[2]/td[2]/a[3]");

        private readonly By rechargeNameLocator = By.XPath("//*[@id=\"dialogConfirmBox\"]/div[2]/div/div[1]/div/input[1]");
        private readonly By rechargeInputLocator = By.XPath("//*[@id=\"scoreRecharge\"]");
        private readonly By rechargeSaveButtonLocator = By.XPath("//*[@id=\"dialogConfirmBox\"]/div[2]/div/div[2]/a[1]");

        private readonly By redeemNameLocator = By.XPath("//*[@id=\"dialogConfirmBox\"]/div[2]/div/div[1]/div/input[1]");
        private readonly By redeemInputLocator = By.XPath("//*[@id=\"scoreRecharge\"]");
        private readonly By redeemSaveButtonLocator = By.XPath("//*[@id=\"dialogConfirmBox\"]/div[2]/div/div[2]/a[1]");

        private string userId;


        public BCLiveShopsManagement(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
        }

        protected override string getBalance()
        {
            var frames = this.driver.FindElements(By.TagName("iframe"));
            this.driver.SwitchTo().Frame(0);
            try
            {
                var agentManagementButtonElement = this.getElementByLocator(this.agentManagementButtonLocator);
                if (agentManagementButtonElement == null)
                {
                    return string.Empty;
                }

                agentManagementButtonElement.Click();
                this.driver.SwitchTo().ParentFrame();
                frames = this.driver.FindElements(By.TagName("iframe"));
                this.driver.SwitchTo().Frame(1);

                var currentBalanceAmountElement = this.getElementByLocator(this.agentManagementBalanceLocator);
                if (currentBalanceAmountElement == null)
                {
                    return "0.00";
                }

                // Find "Your Balance: and strip everthing (including 'Your Balance:') to the left
                var text = currentBalanceAmountElement.Text;
                text = text.Substring(text.IndexOf("Your Balance:", StringComparison.Ordinal) + "Your Balance:".Length);
                var balanceAsString = text.Replace(" ", string.Empty);
                return balanceAsString;
            }
            finally
            {
                this.driver.SwitchTo().ParentFrame();
                var source = this.driver.PageSource;
                frames = this.driver.FindElements(By.TagName("iframe"));
            }
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
        {
            // Have to go to iFrame on the left panel
            this.driver.SwitchTo().Frame(1);

            this.userId = userId;
            var agentManagementButtonElement = this.getElementByLocator(this.agentManagementButtonLocator);
            if (agentManagementButtonElement == null)
            {
                return false;
            }

            agentManagementButtonElement.Click();

            this.driver.SwitchTo().ParentFrame();
            this.driver.SwitchTo().Frame(2);

            var agentAccountsSearchInputElement = this.getElementByLocator(this.agentAccountsSearchInputLocator);
            if (agentAccountsSearchInputElement == null)
            {
                return false;
            }

            agentAccountsSearchInputElement.SendKeys(userId);

            var agentAccountSearchButtonElement = this.getElementByLocator(this.agentAccountSearchButtonLocator);
            if (agentAccountSearchButtonElement == null)
            {
                return false;
            }

            agentAccountSearchButtonElement.Click();


            return true;
        }

        protected override bool makeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            var amountAsPenniesAsDollars = Math.Round(amountAsPennies / 100.0, 2);
            if (amountAsPenniesAsDollars > 0)
            {
                var plusCreditButtonElement = this.getElementByLocator(this.rechargeLinkLocator);
                if (plusCreditButtonElement == null) return false;

                plusCreditButtonElement.Click();

                var nameElement = this.getElementByLocator(this.rechargeNameLocator);
                if (nameElement == null) return false;

                var text = nameElement.GetAttribute("value").ToLower();
                if (text != this.userId.ToLower())
                {
                    return false;
                }

                var rechargeInputElement = this.getElementByLocator(this.rechargeInputLocator);
                if (rechargeInputElement == null) return false;
                rechargeInputElement.SendKeys(amountAsPenniesAsDollars.ToString());

                var rechargeSaveButtonElement = this.getElementByLocator(this.rechargeSaveButtonLocator);
                if (rechargeSaveButtonElement == null) return false;
                // rechargeSaveButtonElement.Click();
            }
            else
            {
                var negativeCreditButtonElement = this.getElementByLocator(this.redeemLinkLocator);
                if (negativeCreditButtonElement == null) return false;

                negativeCreditButtonElement.Click();

                var nameElement = this.getElementByLocator(this.redeemNameLocator);
                if (nameElement == null) return false;
                if (nameElement.Text.ToLower() != this.userId.ToLower())
                {
                    return false;
                }

                var redeemInputElement = this.getElementByLocator(this.redeemInputLocator);
                if (redeemInputElement == null) return false;
                redeemInputElement.SendKeys(amountAsPennies.ToString());

                var redeemSaveButtonElement = this.getElementByLocator(this.redeemSaveButtonLocator);
                if (redeemSaveButtonElement == null) return false;
                // redeemSaveButtonElement.Click();
            }

            return false;
        }

        // For this site, balance is not available on the main screen.
        protected override bool verifyFundsAvailable(int points)
        {
            var balanceAsString = this.getBalance();
            var currentBalance = decimal.Parse(balanceAsString);
            var pointsAsDollars = points * 1.0M / 100.0M;
            return true;
            return currentBalance >= pointsAsDollars;
        }

        protected override bool verifyPageLoaded()
        {
            var result = this.wait.Until(d => this.checkPageSource(d.PageSource));
            return result;
        }

        private bool checkPageSource(string pageSource)
        {
            return pageSource.Contains(this.pageLoadedText);
        }
    }
}