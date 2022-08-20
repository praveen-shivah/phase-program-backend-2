using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class RiverSweepsLogout : BaseLogoutPage
{
    private readonly string pageLoadedText = "";

    private readonly string pageUrl = "https://river-pay.com/office/logout";
    private readonly string loginPageUrl = "https://river-pay.com/office/login";

    [FindsBy(How = How.Name, Using = "yt0")]
    [CacheLookup]
    private IWebElement logIn;
    [FindsBy(How = How.Id, Using = "LoginForm_password")]
    private IWebElement password;

    private readonly int timeout = 15;

    [FindsBy(How = How.XPath, Using = @"//*[@id='yw0']/div/ul/li")]
    private IWebElement errorMessage;

    [FindsBy(How = How.Id, Using = "LoginForm_login")]
    private IWebElement userName;

    public RiverSweepsLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        webDriver.Url = this.pageUrl;
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.loginPageUrl); });
        return true;
    }
}