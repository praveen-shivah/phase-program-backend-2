namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using OpenQA.Selenium;

    public class LogoutPageFactory : ILogoutPageFactory
    {
        ILogoutPage ILogoutPageFactory.Create(IWebDriver webDriver, SoftwareTypeEnum softwareType)
        {
            switch (softwareType)
            {
                case SoftwareTypeEnum.riverSweeps:
                    return new RiverSweepsLogout(webDriver);
                case SoftwareTypeEnum.ultraPanda:
                    return new UltraPandaLogout(webDriver);
                case SoftwareTypeEnum.vBlink:
                    return new VBlinkLogout(webDriver);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXLogout(webDriver);
                case SoftwareTypeEnum.grandX:
                    return new GrandXLogout(webDriver);
                case SoftwareTypeEnum.gaminator:
                    return new GaminatorG3Logout(webDriver);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarLogout(webDriver);
                case SoftwareTypeEnum.goldenBuffalo:
                    return new GoldenBuffaloLogout(webDriver);
                case SoftwareTypeEnum.poseidonX:
                    return new PoseidonXLogout(webDriver);
                case SoftwareTypeEnum.bcLive:
                    return new BCLiveLogout(webDriver);
                case SoftwareTypeEnum.goldenDragon:
                    return new GoldenDragonLogout(webDriver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}