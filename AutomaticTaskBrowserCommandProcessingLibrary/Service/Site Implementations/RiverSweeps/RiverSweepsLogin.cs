using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class RiverSweepsLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "";

    private readonly string pageUrl = "https://river-pay.com/office/login";

    private By errorMessageLocator = By.XPath(@"//*[@id='yw0']/div/ul/li");

    private readonly By loginBtnLocator = By.Name("yt0");

    private readonly By passwordLocator = By.Id("LoginForm_password");

    private readonly By userNameLocator = By.Id("LoginForm_login");

    public RiverSweepsLogin(IWebDriver driver, LoginPageInformation loginPageInformation)
        : base(driver, loginPageInformation)
    {
        driver.Url = this.pageUrl;
        PageFactory.InitElements(driver, this);
    }

    protected override bool submit(IWebDriver driver, LoginPageInformation loginPageInformation)
    {
        var userName = this.getElementByLocator(this.userNameLocator);
        var password = this.getElementByLocator(this.passwordLocator);
        userName.SendKeys(loginPageInformation.SiteUserId);
        password.SendKeys(loginPageInformation.SitePassword);

        this.clickLogInButton();

        this.wait.Until(d => driver.Url != this.pageUrl);

        return driver.Url != this.pageUrl;
    }

    protected override bool verifyPageLoaded(IWebDriver driver)
    {
        this.wait.Until(d => { return d.PageSource.Contains(this.pageLoadedText); });
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.pageUrl); });
        return true;
    }

    /// <summary>
    ///     Click on Log In Button.
    /// </summary>
    /// <returns>The LoginPage class instance.</returns>
    private void clickLogInButton()
    {
        var loginBtn = this.getElementByLocator(this.loginBtnLocator);
        loginBtn.Click();
    }
}