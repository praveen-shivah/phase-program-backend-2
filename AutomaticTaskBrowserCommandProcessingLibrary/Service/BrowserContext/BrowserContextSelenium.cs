namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using LoggingLibrary;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    public class BrowserContextSelenium : IBrowserContext
    {
        private readonly ILogger logger;

        private IWebDriver webDriver;

        private WebDriverWait waitForTenSeconds;

        private string currentUrl = string.Empty;

        public BrowserContextSelenium(ILogger logger)
        {
            this.logger = logger;
        }

        bool IBrowserContext.Stop()
        {
            this.webDriver.Quit();
            return true;
        }

        bool IBrowserContext.NavigateToPage(string url, bool forceRefresh)
        {
            try
            {
                if (forceRefresh || url != this.currentUrl)
                {
                    this.webDriver.Navigate().GoToUrl(url);
                    this.currentUrl = url;
                }
            }
            catch (Exception e)
            {
                this.logger.Error(LogClass.General, "BrowserContextSelenium", "NavigateToPage", $"Error navigating to page {url}", e);
                return false;
            }

            return true;
        }

        bool IBrowserContext.Start()
        {
            try
            {
                this.webDriver = new ChromeDriver();
                this.waitForTenSeconds = new WebDriverWait(this.webDriver, TimeSpan.FromSeconds(10));
            }
            catch (Exception e)
            {
                this.logger.Error(LogClass.General, "BrowserContextSelenium", "Start", "Error starting ChromeDriver", e);
                return false;
            }

            return true;
        }
    }
}
