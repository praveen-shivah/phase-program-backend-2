using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class VegasXLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://river-pay.com/office/logout";

    private By logOutButtonLocator = By.XPath("");
    private By okButtonLocator = By.XPath("");

    public VegasXLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        var logOutButton = this.getElementByLocator(this.logOutButtonLocator);
        logOutButton.Click();

        var okButton = this.getElementByLocator(this.okButtonLocator);
        okButton.Click();

        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}