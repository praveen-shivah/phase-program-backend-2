using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class RiverSweepsVendorToOperatorTransferLogin : BaseVendorToOperatorTransferLogin
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
        : base(driver, vendorToOperatorSendPointsTransferRequest)
    {
        this.driver = driver;
        this.vendorToOperatorSendPointsTransferRequest = vendorToOperatorSendPointsTransferRequest;
        this.driver.Url = this.pageUrl;
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

    protected override IVendorToOperatorTransferManagementPage? submit()
    {
        this.userName.SendKeys(this.vendorToOperatorSendPointsTransferRequest.SiteUserId);
        this.password.SendKeys(this.vendorToOperatorSendPointsTransferRequest.SitePassword);
        this.clickLogInButton();

        IVendorToOperatorTransferManagementPage result = new RiverSweepsVendorToOperatorTransferShopsManagement(this.driver);
        if (result.IsPageUrlSet() && result.VerifyPageLoaded())
        {
            return result;
        }

        return null;
    }

    protected override bool verifyPageLoaded()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.PageSource.Contains(this.pageLoadedText); });
        return true;
    }

    protected override bool verifyPageUrl()
    {
        new WebDriverWait(this.driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.pageUrl); });
        return true;
    }
}