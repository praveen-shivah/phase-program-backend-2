using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.PageObjects;

public class UltraMonsterLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://river-pay.com/office/logout";

    [FindsBy(How = How.XPath, Using = @"/html/body/div[1]/div/div[1]/div/div[1]/div/ul/li[2]/button")]
    [CacheLookup]
    private IWebElement logOutButton;

    private readonly int timeout = 15;

    [FindsBy(How = How.XPath, Using = @"/html/body/div[3]/div/div[3]/button[2]")]
    private IWebElement okButton;

    public UltraMonsterLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        this.logOutButton.Click();

        var wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(CommandProcessingConstants.WEB_DRIVER_WAIT_TIMEOUT_SECONDS));
        var result = wait.Until(_ => this.okButton.Displayed);
        this.okButton.Click();

        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        new WebDriverWait(driver, TimeSpan.FromSeconds(this.timeout)).Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}