using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class VBlinkLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://gm.vblink777.club/#/manage-user/search";

    private By logOutButtonLocator = By.XPath("");
    private By okButtonLocator = By.XPath("");

    public VBlinkLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}