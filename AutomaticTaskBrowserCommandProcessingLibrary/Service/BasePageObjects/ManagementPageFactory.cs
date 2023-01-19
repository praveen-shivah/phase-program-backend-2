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
                case SoftwareTypeEnum.ultraPanda:
                    return new UltraPandaShopsManagement(webDriver);
                case SoftwareTypeEnum.vPower:
                    return new VPowerShopsManagement(webDriver);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXShopsManagement(webDriver);
                case SoftwareTypeEnum.grandX:
                    return new GrandXShopsManagement(webDriver);
                case SoftwareTypeEnum.gaminator:
                    return new GaminatorG3ShopsManagement(webDriver);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarShopsManagement(webDriver);
                case SoftwareTypeEnum.goldenBuffalo:
                    return new GoldenBuffaloShopsManagement(webDriver);
                case SoftwareTypeEnum.poseidonX:
                    return new PoseidonXShopsManagement(webDriver);
                case SoftwareTypeEnum.bcLive:
                    return new BCLiveShopsManagement(webDriver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}