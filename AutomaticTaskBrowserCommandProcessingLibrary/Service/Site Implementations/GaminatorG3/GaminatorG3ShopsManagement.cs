namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class GaminatorG3ShopsManagement : BaseManagementPage
    {
        private readonly string pageLoadedText = "Dashboard";

        private readonly string pageUrl = "https://gaminator3.com/configurator/dashboard/index";

        private readonly By searchFolderButtonElementLocator = By.XPath("//*[@id=\"node-breadcrumbs\"]/li");
        private readonly By searchInputBoxElementLocator = By.XPath("//*[@id=\"tree-search\"]");
        private readonly By searchButtonHallElementLocator = By.XPath("//*[@id=\"node-tree\"]/ul[2]/li/ul/li[1]/ul/li[3]/span/span[3]");
        private readonly By searchButtonAgentElementLocator = By.XPath("//*[@id=\"node-tree\"]/ul[2]/li/ul/li[1]/span/span[3]");
        
        private readonly By plusCurrencySelectorElementLocator = By.XPath("//*[@id=\"ChangeNodeLimit_currency_id\"]");
        private readonly By plusCreditButtonLocator = By.XPath("//*[@id=\"operation-panel-content\"]/a[2]");
        private readonly By plusCreditInputElementLocator = By.XPath("//*[@id=\"ChangeNodeLimit_amount\"]");
        private readonly By plusCreditInputButtonElementLocator = By.XPath("//*[@id=\"change-node-limit\"]/div[4]/div/button");

        private readonly By negativeCurrencySelectorElementLocator = By.XPath("//*[@id=\"ChangeNodeLimit_currency_id\"]");
        private readonly By negativeCreditButtonLocator = By.XPath("//*[@id=\"operation-panel-content\"]/a[3]");
        private readonly By negativeCreditInputElementLocator = By.XPath("//*[@id=\"ChangeNodeLimit_amount\"]");
        private readonly By negativeCreditInputButtonElementLocator = By.XPath("//*[@id=\"change-node-limit\"]/div[4]/div/button");

        private readonly By currentBalanceAmountElementLocator = By.XPath("//*[@id='isideScore']/div[1]/small");
        private readonly By shopUserNameElementLocator = By.XPath("//*[@id='userForm']/div[2]/table/tbody/tr/td[2]/a");


        public GaminatorG3ShopsManagement(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
        }

        protected override string getBalance()
        {
            var currentBalanceAmountElement = this.getElementByLocator(this.currentBalanceAmountElementLocator);
            if (currentBalanceAmountElement == null)
            {
                return "0.00";
            }

            var balanceAsString = currentBalanceAmountElement.Text.Replace(" ", string.Empty).Replace("Credits:", string.Empty);
            return balanceAsString;
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
        {
            var searchFolderButtonElement = this.getElementByLocator(this.searchFolderButtonElementLocator);
            if (searchFolderButtonElement == null)
            {
                return false;
            }

            searchFolderButtonElement.Click();

            var searchInputBoxElement = this.getElementByLocator(this.searchInputBoxElementLocator);
            if (searchInputBoxElement == null)
            {
                return false;
            }

            searchInputBoxElement.SendKeys(userId);


            var searchButtonElement = this.getElementByLocators(this.searchButtonHallElementLocator, this.searchButtonAgentElementLocator);
            if (searchButtonElement == null)
            {
                return false;
            }

            searchButtonElement.Click();


            return true;
        }

        protected override bool makeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            var amountAsPenniesAsDollars = Math.Round(amountAsPennies / 100.0, 2);
            if (amountAsPenniesAsDollars > 0)
            {
                var plusCreditButtonElement = this.getElementByLocator(this.plusCreditButtonLocator);
                if (plusCreditButtonElement == null) return false;

                plusCreditButtonElement.Click();

                var currency = this.getElementByLocator(this.plusCurrencySelectorElementLocator);
                if(currency == null) return false;

                currency.SendKeys(Keys.Down);
                currency.Click();
                currency.SendKeys(Keys.Return);

                var plus = this.getElementByLocator(this.plusCreditInputElementLocator);
                if (plus == null) return false;
                plus.SendKeys(amountAsPenniesAsDollars.ToString());

                var plusButton = this.getElementByLocator(this.plusCreditInputButtonElementLocator);
                if(plusButton == null) return false;
                plusButton.Click();
            }
            else
            {
                var negativeCreditButtonElement = this.getElementByLocator(this.negativeCreditButtonLocator);
                if (negativeCreditButtonElement == null) return false;

                negativeCreditButtonElement.Click();

                var currency = this.getElementByLocator(this.negativeCurrencySelectorElementLocator);
                if (currency == null) return false;

                currency.SendKeys(Keys.Down);
                currency.Click();
                currency.SendKeys(Keys.Return);
                
                var negative = this.getElementByLocator(this.negativeCreditInputElementLocator);
                if (negative == null) return false;
                negative.SendKeys(amountAsPenniesAsDollars.ToString());

                var negativeButton = this.getElementByLocator(this.negativeCreditInputButtonElementLocator);
                if (negativeButton == null) return false;
                negativeButton.Click();
            }

            return false;
        }

        // For this site, balance is not available on the main screen.
        protected override bool verifyFundsAvailable(int points)
        {
            return true;

            var currentBalanceAmountElement = this.getElementByLocator(this.currentBalanceAmountElementLocator);
            if (currentBalanceAmountElement == null)
            {
                return false;
            }

            var balanceAsString = currentBalanceAmountElement.Text.Replace(" ", string.Empty).Replace("Credits:", string.Empty);
            var currentBalance = decimal.Parse(balanceAsString);
            var pointsAsDollars = points * 1.0M / 100.0M;
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