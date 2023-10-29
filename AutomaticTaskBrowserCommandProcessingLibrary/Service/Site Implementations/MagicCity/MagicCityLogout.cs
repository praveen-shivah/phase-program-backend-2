using AutomaticTaskBrowserCommandProcessingLibrary;

using OpenQA.Selenium;

using SeleniumExtras.PageObjects;

public class MagicCityLogout : BaseLogoutPage
{
    private readonly string logoutPageUrl = "https://pos.magiccity777.com/pos/login";

    private By logOutButtonLocator = By.XPath("//*[@id=\"__layout\"]/section/div[1]/header/div[1]/div[2]/button/span/span");
    
    private By logOutButtonLocator1 = By.XPath("//*[@id=\"__layout\"]/section/div/div/form/button[2]/span");
    public MagicCityLogout(IWebDriver driver) : base(driver)
    {
        PageFactory.InitElements(driver, this);
    }

    protected override bool logout(IWebDriver webDriver)
    {
        var logOutButton = this.getElementByLocator(this.logOutButtonLocator);
        logOutButton.Click();

        var logOutButton1 = this.getElementByLocator(this.logOutButtonLocator1);
        if(logOutButton1 != null)
        {
            logOutButton1?.Click();
        }
        
        return true;
    }

    protected override bool verifyPageUrl(IWebDriver webDriver)
    {
        this.wait.Until(d => { return d.Url.Contains(this.logoutPageUrl); });
        return true;
    }
}

