using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class RiverSweepsVendorToOperatorTransferLogin
{
    private readonly string pageLoadedText = "";

    private readonly string pageUrl = "https://river-pay.com/office/login";

    private readonly IWebDriver driver;

    private readonly VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest;

    [FindsBy(How = How.Name, Using = "yt0")]
    [CacheLookup]
    private IWebElement logIn;
    [FindsBy(How = How.Id, Using = "LoginForm_password")]
    private IWebElement password;

    private readonly int timeout = 15;

    [FindsBy(How = How.Id, Using = "LoginForm_login")]
    private IWebElement userName;

    public RiverSweepsVendorToOperatorTransferLogin(
        IWebDriver driver,
        VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
    {
        this.driver = driver;
        this.vendorToOperatorSendPointsTransferRequest = vendorToOperatorSendPointsTransferRequest;
        this.driver.Url = this.pageUrl;
        PageFactory.InitElements(driver, this);
    }

    /// <summary>
    ///     Click on Log In Button.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsVendorToOperatorTransferLogin ClickLogInButton()
    {
        this.logIn.Click();

        return this;
    }

    /// <summary>
    ///     Submit the form to target page.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsVendorToOperatorTransferShopsManagement Submit()
    {
        this.userName.SendKeys(this.vendorToOperatorSendPointsTransferRequest.SiteUserId);
        this.password.SendKeys(this.vendorToOperatorSendPointsTransferRequest.SitePassword);
        this.ClickLogInButton();

        var result = new RiverSweepsVendorToOperatorTransferShopsManagement(this.driver);
        if (result.IsPageUrlSet() && result.VerifyPageLoaded())
        {
            return result;
        }

        return null;
    }

    /// <summary>
    ///     Verify that the page loaded completely.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsVendorToOperatorTransferLogin VerifyPageLoaded()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.PageSource.Contains(this.pageLoadedText); });
        return this;
    }

    /// <summary>
    ///     Verify that current page URL matches the expected URL.
    /// </summary>
    /// <returns>The Login class instance.</returns>
    public RiverSweepsVendorToOperatorTransferLogin VerifyPageUrl()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.pageUrl); });
        return this;
    }
}