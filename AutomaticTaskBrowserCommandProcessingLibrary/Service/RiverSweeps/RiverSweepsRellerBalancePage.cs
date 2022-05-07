namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class RiverSweepsRellerBalancePage : BaseResellerBalancePage
    {
        private readonly IWebDriver driver;

        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://river-pay.com/agent/show";

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[2]/div/div[1]/div[1]/p/b")]
        [CacheLookup]
        private IWebElement currentBalanceAmount;

        
        public RiverSweepsRellerBalancePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        protected override string getBalance()
        {
            return this.currentBalanceAmount.Text.Replace(" ", string.Empty).Replace("usd", string.Empty);
        }

        protected override bool isPageUrlSet()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.Url.Contains(this.pageUrl));
            return result;
        }

        protected override bool verifyPageLoaded()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}