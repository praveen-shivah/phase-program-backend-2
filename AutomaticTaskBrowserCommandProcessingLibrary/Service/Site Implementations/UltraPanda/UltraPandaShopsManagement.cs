﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    using SeleniumExtras.PageObjects;

    public class UltraPandaShopsManagement : BaseManagementPage
    {
        private readonly string pageLoadedText = "Agent account";

        private readonly string pageUrl = "https://ht.ultrapanda.mobi/#/manage-user/search";

        private By currentBalanceAmountElementLocator = By.XPath("//*[@id='app']/div/div[2]/div/div[1]/div[3]/p");
        private By setPointsInputELementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[4]/div/form/div[3]/div/div/input");
        private By setPointsRemarksInputELementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[4]/div/form/div[4]/div/div/textarea");
        private By setScoreButtonLocator = By.XPath("/html/body/div/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[16]/div/button[1]");
        private By okButtonLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[1]/form/div[3]/div/button");
        private By searchInputBoxElementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[1]/form/div[2]/div/div/div/div[1]/input");
        private By userAccountIdElementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[4]/div/div/div/div/div[3]/table/tbody/tr/td[2]/div/a/span");

        private By okSetScoreOkButtonLocator = By.XPath("/html/body/div/div/div[2]/section/div/div[4]/div/form/div[5]/div/button[1]");

        private By agentAccountRadioButtonElementLocator = By.XPath("//*[@id='app']/div/div[2]/section/div/div[1]/form/div[1]/div/div/label[1]/span[2]");

        public UltraPandaShopsManagement(IWebDriver driver)
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

            var balanceAsString = currentBalanceAmountElement.Text.Replace(" ", string.Empty).Replace("Myscore:", string.Empty);
            return balanceAsString;
        }

        protected override bool isPageUrlSet()
        {
            var result = this.wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool locateDepositButtonAndClick(string userId)
        {
            var agentAccountRadioButtonElement = this.getElementByLocator(this.agentAccountRadioButtonElementLocator);
            if (agentAccountRadioButtonElement == null)
            {
                return false;
            }

            agentAccountRadioButtonElement.Click();

            var searchInputBoxElement = this.getElementByLocator(this.searchInputBoxElementLocator);
            if (searchInputBoxElement == null)
            {
                return false;
            }

            searchInputBoxElement.SendKeys(userId);

            var okButton = this.getElementByLocator(this.okButtonLocator);
            if (okButton == null)
            {
                return false;
            }

            okButton.Click();

            var userAccountIdElement = this.getElementByLocator(userAccountIdElementLocator);
            if (userAccountIdElement == null)
            {
                return false;
            }

            var test = userAccountIdElement.Text.ToLower();
            if (test != userId.ToLower())
            {
                return false;
            }

            var setScoreButton = this.getElementByLocator(this.setScoreButtonLocator);
            if (setScoreButton == null)
            {
                return false;
            }

            setScoreButton.Click();

            return true;
        }

        protected override bool makeDeposit(int amountAsPennies, string invoiceLineItemId)
        {
            var setPointsInputElement = this.getElementByLocator(this.setPointsInputELementLocator);
            if (setPointsInputElement == null)
            {
                return false;
            }

            var amountAsPenniesAsDollars = Math.Round(amountAsPennies / 100.0, 2);
            setPointsInputElement.SendKeys(amountAsPenniesAsDollars.ToString());

            var setPointsRemarksInputElement = this.getElementByLocator(this.setPointsRemarksInputELementLocator);
            if (setPointsRemarksInputElement == null)
            {
                return false;
            }

            setPointsRemarksInputElement.SendKeys($"Invoice line item id: {invoiceLineItemId}");

            var okSetScoreOkButtonElement = this.getElementByLocator(this.okSetScoreOkButtonLocator);
            if (okSetScoreOkButtonElement == null)
            {
                return false;
            }

            okSetScoreOkButtonElement.Click();

            return true;
        }

        protected override bool verifyFundsAvailable(int points)
        {
            var currentBalanceAmountElement = this.getElementByLocator(this.currentBalanceAmountElementLocator);
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
            var result = this.wait.Until(d => this.checkPageSource(d.PageSource));
            return result;
        }

        private bool checkPageSource(string pageSource)
        {
            return pageSource.Contains(this.pageLoadedText);
        }
    }
}