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
                case SoftwareTypeEnum.ultraPanda:
                    return new UltraPandaLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.vBlink:
                    return new VBlinkLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.grandX:
                    return new GrandXLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.gaminator:
                    return new GaminatorG3Login(webDriver, loginPageInformation);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.goldenBuffalo:
                    return new GoldenBuffaloLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.poseidonX:
                    return new PoseidonXLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.bcLive:
                    return new BCLiveLogin(webDriver, loginPageInformation);
                case SoftwareTypeEnum.goldenDragon:
                    return new GoldenDragonLogin(webDriver, loginPageInformation);

                default:
                    throw new ArgumentOutOfRangeException(nameof(loginPageInformation.SoftwareType), loginPageInformation.SoftwareType, null);
            }
        }
    }
}