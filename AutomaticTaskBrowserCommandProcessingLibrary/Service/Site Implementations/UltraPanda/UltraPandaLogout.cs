using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class UltraPandaLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://ht.ultrapanda.mobi/#/index";

    private By logoutBtnLocator = By.XPath(@"/html/body/div[1]/div/div[1]/div/div[1]/div/ul/li[2]/button");
    private By okBtnLocator = By.XPath(@"/html/body/div[3]/div/div[3]/button[2]");


    public UltraPandaLogout(IWebDriver driver)
        : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        return true;
        var logoutBtn = this.getElementByLocator(this.logoutBtnLocator);
        logoutBtn.Click();

        var okButton = this.getElementByLocator(this.okBtnLocator);
        okButton.Click();

        return true;
    }

    protected override bool verifyPageUrl(IWebDriver driver)
    {
        // this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}