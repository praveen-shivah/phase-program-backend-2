using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class RiverSweepsLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "";

    private readonly string pageUrl = "https://river-pay.com/office/login";

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

    public RiverSweepsLogin(
        IWebDriver driver,
        LoginPageInformation loginPageInformation)
        : base(driver, loginPageInformation)
    {
        driver.Url = this.pageUrl;
        PageFactory.InitElements(driver, this);
    }

    /// <summary>
    ///     Click on Log In Button.
    /// </summary>
    /// <returns>The LoginPage class instance.</returns>
    private void clickLogInButton()
    {
        this.logIn.Click();
    }

    protected override bool submit(IWebDriver driver, LoginPageInformation loginPageInformation)
    {
        this.userName.SendKeys(loginPageInformation.SiteUserId);
        this.password.SendKeys(loginPageInformation.SitePassword);
        this.clickLogInButton();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => driver.Url != this.pageUrl || this.errorMessage.Displayed);

        return driver.Url != this.pageUrl;
    }

    protected override bool verifyPageLoaded(IWebDriver driver)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.PageSource.Contains(this.pageLoadedText); });
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.pageUrl); });
        return true;
    }
}