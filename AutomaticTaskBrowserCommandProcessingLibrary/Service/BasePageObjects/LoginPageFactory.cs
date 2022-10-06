namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using OpenQA.Selenium;

    public class LoginPageFactory : ILoginPageFactory
    {
        ILoginPage ILoginPageFactory.Create(IWebDriver webDriver, LoginPageInformation loginPageInformation)
        {
            switch (loginPageInformation.SoftwareType)
            {
                case SoftwareTypeEnum.riverSweeps:
                    return new RiverSweepsLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.ultraMonster:
                    return new UltraMonsterLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.vPower:
                    return new VPowerLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.grandX:
                    return new GrandXLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarLogin(webDriver, loginPageInformation);
                default:
                    throw new ArgumentOutOfRangeException(nameof(loginPageInformation.SoftwareType), loginPageInformation.SoftwareType, null);
            }
        }
    }
}