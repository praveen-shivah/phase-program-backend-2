using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class PoseidonXLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://poseidonx.xyz:8781/LoginOut.aspx";

    public PoseidonXLogout(IWebDriver driver)
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
        this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}