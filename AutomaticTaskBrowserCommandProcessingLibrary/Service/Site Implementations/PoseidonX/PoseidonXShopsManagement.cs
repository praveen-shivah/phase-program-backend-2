namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class PoseidonXShopsManagement : BaseManagementPage
    {
        private readonly string pageLoadedText = "Balance";

        private readonly string pageUrl = "https://poseidonx.xyz:8781/Agent.aspx";

        private readonly By adminStructureElementLocator = By.XPath("/html/body/div/div[1]/a");
        private readonly By adminStructureAccountSearchInputElementLocator = By.Id("txtSearch");

        private readonly By searchButtonElementLocator = By.Id("Button4");

        private readonly By updateButtonElementLocator = By.XPath("//*[@id=\"rptUser_ctl00_Button1\"]");
        private readonly By accountTextElementLocator = By.XPath("//*[@id=\"form1\"]/div[3]/table[1]/tbody/tr[2]/td[2]");

        private readonly By plusCreditButtonLocator = By.XPath("//*[@id=\"form1\"]/div[2]/div[5]/div/a[1]");
        private readonly By plusCreditInputElementLocator = By.Id("txtAddGold");
        private readonly By plusCreditInputButtonElementLocator = By.Id("Button1");

        private readonly By negativeCreditButtonLocator = By.XPath("//*[@id=\"form1\"]/div[2]/div[5]/div/a[1]");
        private readonly By negativeCreditInputElementLocator = By.Id("txtAddGold");
        private readonly By negativeCreditInputButtonElementLocator = By.Id("Button1");

        private readonly By currentBalanceAmountElementLocator = By.Id("UserBalance");
        private readonly By shopUserNameElementLocator = By.XPath("//*[@id='userForm']/div[2]/table/tbody/tr/td[2]/a");


        public PoseidonXShopsManagement(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
        }

        protected override string getBalance()
        {
            try
            {
                var currentBalanceAmountElement = this.getElementByLocator(this.currentBalanceAmountElementLocator);
                if (currentBalanceAmountElement == null)
                {
                    return "0.00";
                }

                // Find "Your Balance: and strip everthing (including 'Your Balance:') to the left
                var text = currentBalanceAmountElement.Text;
                text = text.Substring(text.IndexOf("Balance:", StringComparison.Ordinal) + "Balance:".Length);
                var balanceAsString = text.Replace(" ", string.Empty);
                return balanceAsString;
            }
            finally
            {
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
            this.driver.SwitchTo().Frame(0);
            try
            {
                var adminStructureElement = this.getElementByLocator(this.adminStructureElementLocator);
                if (adminStructureElement == null)
                {
                    return false;
                }

                adminStructureElement.Click();

                this.driver.SwitchTo().ParentFrame();
                this.driver.SwitchTo().Frame(1);
                var adminStructureAccountSearchInputElement = this.getElementByLocator(this.adminStructureAccountSearchInputElementLocator);
                if (adminStructureAccountSearchInputElement == null)
                {
                    return false;
                }

                adminStructureAccountSearchInputElement.SendKeys(userId);

                var searchButtonElement = this.getElementByLocator(this.searchButtonElementLocator);
                if (searchButtonElement == null)
                {
                    return false;
                }

                searchButtonElement.Click();

                var updateButtonElement = this.getElementByLocator(this.updateButtonElementLocator);
                if (updateButtonElement == null) return false;
                var accountTextElement = this.getElementByLocator(this.accountTextElementLocator);
                if (accountTextElement == null) return false;

                var text = accountTextElement.Text;
                if (text.ToLower().Trim() != userId.ToLower().Trim()) return false;

                this.driver.SwitchTo().ParentFrame();
                var frames = this.driver.FindElements(By.TagName("iframe"));
                this.driver.SwitchTo().Frame(1);

                updateButtonElement.Click();

                return true;
            }
            finally
            {
                this.driver.SwitchTo().ParentFrame();
            }
        }

        protected override bool makeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            var frames = this.driver.FindElements(By.TagName("iframe"));
            this.driver.SwitchTo().Frame(1);

            if (amountAsPennies > 0)
            {
                var plusCreditButtonElement = this.getElementByLocator(this.plusCreditButtonLocator);
                if (plusCreditButtonElement == null) return false;

                plusCreditButtonElement.Click();

                this.driver.SwitchTo().ParentFrame();
                frames = this.driver.FindElements(By.TagName("iframe"));
                this.driver.SwitchTo().Frame(2);


                var plus = this.getElementByLocator(this.plusCreditInputElementLocator);
                if (plus == null) return false;
                plus.SendKeys(amountAsPennies.ToString());

                var plusButton = this.getElementByLocator(this.plusCreditInputButtonElementLocator);
                if (plusButton == null) return false;
                plusButton.Click();
            }
            else
            {
                var negativeCreditButtonElement = this.getElementByLocator(this.negativeCreditButtonLocator);
                if (negativeCreditButtonElement == null) return false;

                negativeCreditButtonElement.Click();

                this.driver.SwitchTo().ParentFrame();
                frames = this.driver.FindElements(By.TagName("iframe"));
                this.driver.SwitchTo().Frame(2);


                var negative = this.getElementByLocator(this.negativeCreditInputElementLocator);
                if (negative == null) return false;
                negative.SendKeys(amountAsPennies.ToString());

                var negativeButton = this.getElementByLocator(this.negativeCreditInputButtonElementLocator);
                if (negativeButton == null) return false;
                negativeButton.Click();
            }

            return false;
        }

        protected override bool verifyFundsAvailable(int points)
        {
            var balanceAsString = this.getBalance();
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