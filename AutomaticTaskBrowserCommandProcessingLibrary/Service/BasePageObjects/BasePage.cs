namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    using SeleniumExtras.WaitHelpers;

    public class BasePage
    {
        protected readonly IWebDriver driver;

        protected readonly WebDriverWait wait;

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
            this.driver.Manage().Window.Maximize();
        }

        protected IWebElement? getElementByLocator(By locator)
        {
            try
            {
                return this.wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch
            {
            }

            return null;
        }

        protected IWebElement? getElementByLocators(By locator1, By locator2)
        {
            try
            {
                return this.wait.Until(this.OneElementIsVisible(locator1, locator2));
            }
            catch
            {
            }

            return null;
        }

        private static IWebElement? elementIfVisible(IWebElement element) => !element.Displayed ? null : element;

        private Func<IWebDriver, IWebElement?> OneElementIsVisible(By locator1, By locator2)
        {
            return driver =>

                {
                    try
                    {
                        if (elementIfVisible(driver.FindElement(locator1)) != null)
                        {
                            return driver.FindElement(locator1);
                        }

                        if (elementIfVisible(driver.FindElement(locator2)) != null)
                        {
                            return driver.FindElement(locator2);
                        }

                        return null;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return null;
                    }
                };
        }
    }
}