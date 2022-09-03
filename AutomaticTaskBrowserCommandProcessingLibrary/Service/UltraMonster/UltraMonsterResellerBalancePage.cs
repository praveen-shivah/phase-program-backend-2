namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.PageObjects;

    public class UltraMonsterResellerBalancePage : BaseResellerBalancePage
    {
        private readonly IWebDriver driver;

        private readonly string pageLoadedText = "</body>";

        private readonly string pageUrl = "https://go123.ultramonster.net/#/index";

        [FindsBy(How = How.XPath, Using = @"/html/body/div[1]/div/div[2]/div/div[1]/div[3]/p/span")]
        [CacheLookup]
        private IWebElement? currentBalanceAmount;

        public UltraMonsterResellerBalancePage(IWebDriver driver)
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

        protected override bool verifyPageLoaded()
        {
            var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            var result = wait.Until(d => d.PageSource.Contains(this.pageLoadedText));
            return result;
        }
    }
}