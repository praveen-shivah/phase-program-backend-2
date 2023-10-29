using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class FirestormLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://pos.firestormhub.com/pos/9797040";

    private By logOutButtonLocator = By.XPath("//*[@id=\"Logout\"]/a");

    public FirestormLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        var logOutButton = this.getElementByLocator(this.logOutButtonLocator);
        logOutButton.Click();

        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}