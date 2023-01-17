using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class RiverSweepsLogout : BaseLogoutPage
{
    private readonly string loginPageUrl = "https://river-pay.com/office/login";
    private readonly string logoutPageUrl = "https://river-pay.com/office/logout";

    public RiverSweepsLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        webDriver.Url = this.logoutPageUrl;
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.loginPageUrl); });
        return true;
    }
}