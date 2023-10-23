using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;


public class MagicCityLogin : BaseLoginPage
{
    private readonly string pageLoadedText = "Log In";

    private readonly string pageUrl = "https://pos.magiccity777.com/";
    

    private By userNameLocator = By.XPath("//*[@id=\"__layout\"]/section/div/div/form/div[1]/div/div/input");
    private By passwordLocator = By.XPath("//*[@id=\"__layout\"]/section/div/div/form/div[2]/div/div/input");
    private By loginBtnLocator = By.XPath("//*[@id=\"__layout\"]/section/div/div/form/button/span");
    private By drawerElement = By.XPath("//*[@id=\"__layout\"]/section/div/div/form/div[2]/div/div/div/input");
    private By selectBtnLocator = By.XPath("//*[@id=\"__layout\"]/section/div/div/form/button[1]/span");
    public MagicCityLogin(IWebDriver driver, LoginPageInformation loginPageInformation) : base(driver, loginPageInformation)
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
        //this.SetIframe(0);
        var userNameElement = this.getElementByLocator(this.userNameLocator);
        var passwordElement = this.getElementByLocator(this.passwordLocator);
        userNameElement.SendKeys(loginPageInformation.SiteUserId);
        passwordElement.Click();
        passwordElement.Clear();
        passwordElement.SendKeys(loginPageInformation.SitePassword);
        this.clickLogInButton();
        this.wait.Until(d => d.PageSource.Contains("Kiosk"));
        var drawerElement = this.getElementByLocator(this.drawerElement);
        if (drawerElement != null)
        {
            drawerElement.Click();
            this.wait.Until(d => d.PageSource.Contains("View"));
            var drawerDropdownElement = this.getElementByLocator(By.XPath($"/html/body/div[2]/div[1]/div[1]/ul/li[{loginPageInformation.Drawer}]/span"));
            drawerDropdownElement.Click();
            var selectElement = this.getElementByLocator(this.selectBtnLocator);
            selectElement.Click();
        }
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
