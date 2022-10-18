namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class GrandXShopsManagement : BaseManagementPage
    {
        private readonly string pageLoadedText = "Accounts Limit";

        private readonly string pageUrl = "https://grandx.org/xo/shops";

        private By currentBalanceAmountElementLocator = By.XPath("//*[@id='isideScore']/div[1]/small");
        private By searchInputBoxElementLocator = By.XPath("//*[@id='search-input']");
        private By searchButtonElementLocator = By.XPath("//*[@id='searchbutton']");
        private By shopUserNameElementLocator = By.XPath("//*[@id='userForm']/div[2]/table/tbody/tr/td[2]/a");
        private By setPointsInputELementLocator = By.XPath("//*[@id='score']");
        private By plusCreditButtonLocator = By.XPath("//*[@id='scoreInButton']");
        private By negativeCreditbuttonLocator = By.XPath("//*[@id='scoreOutButton']");


        public GrandXShopsManagement(IWebDriver driver)
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
            var searchInputBoxElement = this.getElementByLocator(this.searchInputBoxElementLocator);
            if (searchInputBoxElement == null)
            {
                return false;
            }

            searchInputBoxElement.Click();
            searchInputBoxElement.SendKeys(userId);

            var searchButtonElement = this.getElementByLocator(this.searchButtonElementLocator);
            if (searchButtonElement == null)
            {
                return false;
            }

            searchButtonElement.Click();


            var shopUserNameElement = this.getElementByLocator(shopUserNameElementLocator);
            if (shopUserNameElement == null)
            {
                return false;
            }

            var test = shopUserNameElement.Text.ToLower();
            if (test != userId.ToLower())
            {
                return false;
            }

            shopUserNameElement.Click();

            return true;
        }

        protected override bool makeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            var setPointsInputELement = this.getElementByLocator(this.setPointsInputELementLocator);
            if (setPointsInputELement == null)
            {
                return false;
            }

            var amountAsPenniesAsDollars = Math.Round(amountAsPennies / 100.0, 2);
            if (amountAsPenniesAsDollars > 0)
            {
                setPointsInputELement.SendKeys(amountAsPenniesAsDollars.ToString());

                var creditbuttonElement = this.getElementByLocator(this.plusCreditButtonLocator);
                if (creditbuttonElement == null)
                {
                    return false;
                }
            }
            else
            {
                amountAsPenniesAsDollars = -amountAsPenniesAsDollars;
                setPointsInputELement.SendKeys(amountAsPenniesAsDollars.ToString());

                var creditbuttonElement = this.getElementByLocator(this.negativeCreditbuttonLocator);
                if (creditbuttonElement == null)
                {
                    return false;
                }

            }

            // this.creditbuttonElement.Click();
            return false;
        }

        protected override bool verifyFundsAvailable(int points)
        {
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