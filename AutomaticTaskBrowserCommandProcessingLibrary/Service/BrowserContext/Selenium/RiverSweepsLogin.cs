using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class RiverSweepsLogin
{
    private readonly string pageLoadedText = "";

    private readonly string pageUrl = "https://river-pay.com/office/login";

    private readonly string passwordValue;

    private readonly string userNameValue;

    private readonly IWebDriver driver;

    [FindsBy(How = How.Name, Using = "yt0")]
    [CacheLookup]
    private IWebElement logIn;
    [FindsBy(How = How.Id, Using = "LoginForm_password")]
    private IWebElement password;

    private readonly int timeout = 15;

    [FindsBy(How = How.Id, Using = "LoginForm_login")]
    private IWebElement userName;

    public RiverSweepsLogin(
        IWebDriver driver,
        string userNameValue,
        string passwordValue)
    {
        this.driver = driver;
        this.driver.Url = this.pageUrl;
        this.userNameValue = userNameValue;
        this.passwordValue = passwordValue;
        PageFactory.InitElements(driver, this);
    }

    /// <summary>
    ///     Click on Log In Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsLogin ClickLogInButton()
    {
        this.logIn.Click();
        return this;
    }

    /// <summary>
    ///     Submit the form to target page.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsLogin Submit()
    {
        this.userName.SendKeys(this.userNameValue);
        this.password.SendKeys(this.passwordValue);
        this.ClickLogInButton();
        return this;
    }

    /// <summary>
    ///     Verify that the page loaded completely.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsLogin VerifyPageLoaded()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.PageSource.Contains(this.pageLoadedText); });
        return this;
    }

    /// <summary>
    ///     Verify that current page URL matches the expected URL.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsLogin VerifyPageUrl()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.pageUrl); });
        return this;
    }
}