using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class PoseidonXLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "Version:";

    private readonly string pageUrl = "https://poseidonx.xyz:8781/default.aspx";

    private By userNameLocator = By.Id("txtLoginName");
    private By passwordLocator = By.Id("txtLoginPass");

    private By loginBtnLocator = By.Id("btnLogin");

    public PoseidonXLogin(
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
        var loginBtnELement = this.getElementByLocator(this.loginBtnLocator);
        loginBtnELement.Click();
    }

    protected override bool submit(IWebDriver driver, LoginPageInformation loginPageInformation)
    {
        var userNameElement = this.getElementByLocator(this.userNameLocator);
        var passwordElement = this.getElementByLocator(this.passwordLocator);
        userNameElement.SendKeys(loginPageInformation.SiteUserId);
        passwordElement.SendKeys(loginPageInformation.SitePassword);

        this.clickLogInButton();

        this.wait.Until(d => driver.Url != this.pageUrl);

        return driver.Url != this.pageUrl;
    }

    protected override bool verifyPageLoaded(IWebDriver driver)
    {
        this.wait.Until(d => { return this.checkPageSource(d.PageSource); });
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.pageUrl); });
        return true;
    }

    private bool checkPageSource(string pageSource)
    {
        return pageSource.Contains(this.pageLoadedText);
    }
}