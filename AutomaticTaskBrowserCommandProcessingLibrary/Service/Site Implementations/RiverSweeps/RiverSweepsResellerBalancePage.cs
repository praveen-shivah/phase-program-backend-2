namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    using SeleniumExtras.PageObjects;

    public class RiverSweepsResellerBalancePage : BaseResellerBalancePage
    {
        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://river-pay.com/agent/show";

        private By currentBalanceAmountLocator = By.XPath("/html/body/div[1]/div[2]/div/div[1]/div[1]/p/b");

        
        public RiverSweepsResellerBalancePage(IWebDriver driver)
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

        protected override bool verifyPageLoaded()
        {
            var result = this.wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}