using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class UltraMonsterLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "passWd";

    private readonly string pageUrl = "https://go123.ultramonster.net/#/login";

    [FindsBy(How = How.XPath, Using = "//*[@id='app']/div/div/form/button[1]")]
    [CacheLookup]
    private IWebElement logIn;

    [FindsBy(How = How.Name, Using = "passWd")]
    private IWebElement password;

    private readonly int timeout = 15;

    [FindsBy(How = How.XPath, Using = @"//*[@id='yw0']/div/ul/li")]
    private IWebElement errorMessage;

    [FindsBy(How = How.Name, Using = "userName")]
    private IWebElement userName;

    public UltraMonsterLogin(
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

        // var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => driver.Url != this.pageUrl || this.errorMessage.Displayed);

        // Impossible to get the error message so just relying on timeout
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => driver.Url != this.pageUrl);

        return driver.Url != this.pageUrl;
    }

    protected override bool verifyPageLoaded(IWebDriver driver)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return this.checkPageSource(d.PageSource); });
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.pageUrl); });
        return true;
    }

    private bool checkPageSource(string pageSource)
    {
        return pageSource.Contains(this.pageLoadedText);
    }
}