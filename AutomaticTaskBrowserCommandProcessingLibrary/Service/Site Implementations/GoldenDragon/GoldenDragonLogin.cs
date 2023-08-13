using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class GoldenDragonLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "Log In";

    private readonly string pageUrl = "https://pos.goldendragoncity.com/Drawer/";

    private By userNameLocator = By.XPath("//*[@id=\"username\"]");
    private By passwordLocator = By.XPath("//*[@id=\"password\"]");
    private By loginBtnLocator = By.XPath("//*[@id=\"login_email\"]");

    public GoldenDragonLogin(
        IWebDriver driver,
        LoginPageInformation loginPageInformation): base(driver, loginPageInformation)
    {
        driver.Url = loginPageInformation.LoginPage;
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
        this.SetIframe(0);
        var userNameElement = this.getElementByLocator(this.userNameLocator);
        var passwordElement = this.getElementByLocator(this.passwordLocator);
        userNameElement.SendKeys(loginPageInformation.SiteUserId);
        passwordElement.Click();
        passwordElement.Clear();
        passwordElement.SendKeys(loginPageInformation.SitePassword);

        this.clickLogInButton();

        this.wait.Until(d => driver.Url.ToString() == this.pageUrl);
        var url = driver.Url.ToString();
        return url == this.pageUrl;
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