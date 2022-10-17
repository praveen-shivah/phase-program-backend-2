﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
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
                case SoftwareTypeEnum.ultraMonster:
                    return new UltraMonsterLogout(webDriver);
                case SoftwareTypeEnum.vPower:
                    return new VPowerLogout(webDriver);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXLogout(webDriver);
                case SoftwareTypeEnum.grandX:
                    return new GrandXLogout(webDriver);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarLogout(webDriver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}