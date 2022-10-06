namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using OpenQA.Selenium;

    public class ManagementPageFactory : IManagementPageFactory
    {
        IManagementPage IManagementPageFactory.Create(IWebDriver webDriver, SoftwareTypeEnum softwareType)
        {
            switch (softwareType)
            {
                case SoftwareTypeEnum.riverSweeps:
                    return new RiverSweepsShopsManagement(webDriver);
                case SoftwareTypeEnum.ultraMonster:
                    return new UltraMonsterShopsManagement(webDriver);
                case SoftwareTypeEnum.vPower:
                    return new VPowerShopsManagement(webDriver);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXShopsManagement(webDriver);
                case SoftwareTypeEnum.grandX:
                    return new GrandXShopsManagement(webDriver);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarShopsManagement(webDriver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}