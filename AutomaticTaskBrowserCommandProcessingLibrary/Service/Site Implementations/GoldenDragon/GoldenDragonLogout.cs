using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class GoldenDragonLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://pos.goldendragoncity.com/pos/4432243";

    private By logOutButtonLocator = By.XPath("//*[@id=\"Logout\"]/a");
    private By logOutYesButtonLocator = By.XPath("//*[@id=\"ajs-ok\"]");

    public GoldenDragonLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        var logOutButton = this.getElementByLocator(this.logOutButtonLocator);
        logOutButton.Click();

        IList<IWebElement> btnCollection = this.driver.FindElements(this.logOutYesButtonLocator);
        foreach (IWebElement btn in btnCollection)
        {
            try
            {
                if (btn.Text == "No, Just Logout")
                {
                    btn.Click();
                    break;
                }
            }
            catch(Exception ex) { }
        }

        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}