﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    using SeleniumExtras.PageObjects;

    public class PoseidonXResellerBalancePage : BaseResellerBalancePage
    {
        private readonly string pageLoadedText = "Agent account";

        private readonly string pageUrl = "https://poseidonx.xyz:8781/Agent.aspx";

        private By currentBalanceAmountElementLocator = By.XPath("//*[@id='app']/div/div[2]/div/div[1]/div[3]/p");

        public PoseidonXResellerBalancePage(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver.Url = this.pageUrl;
        }

        protected override string getBalance()
        {
            var currentBalanceAmountElement = getElementByLocator(this.currentBalanceAmountElementLocator);
            if (currentBalanceAmountElement == null)
            {
                return "0.00";
            }

            var balanceAsString = currentBalanceAmountElement.Text.Replace(" ", string.Empty).Replace("Myscore:", string.Empty);
            return balanceAsString;
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool verifyPageLoaded()
        {
            var result = this.wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}