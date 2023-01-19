using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class UltraPandaLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "passWd";

    private readonly string pageUrl = "https://ht.ultrapanda.mobi/#/login";

    private By loginBtnLocator = By.XPath("//*[@id='app']/div/div/form/button[1]");
    private By passwordLocator = By.Name("passWd");
    private By errorMessageLocator = By.XPath(@"//*[@id='yw0']/div/ul/li");
    private By userNameLocator = By.Name("userName");

    public UltraPandaLogin(
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
        var loginBtn = this.getElementByLocator(this.loginBtnLocator);
        loginBtn.Click();
    }

    protected override bool submit(IWebDriver driver, LoginPageInformation loginPageInformation)
    {
        var userName = this.getElementByLocator(this.userNameLocator);
        var password = this.getElementByLocator(this.passwordLocator);
        userName.SendKeys(loginPageInformation.SiteUserId);
        password.SendKeys(loginPageInformation.SitePassword);
        this.clickLogInButton();

        // var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => driver.Url != this.pageUrl || this.errorMessage.Displayed);

        // Impossible to get the error message so just relying on timeout
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