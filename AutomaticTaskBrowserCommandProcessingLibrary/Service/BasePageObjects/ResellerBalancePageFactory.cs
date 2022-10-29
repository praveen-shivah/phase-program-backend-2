namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium;

    public class ResellerBalancePageFactory : IResellerBalancePageFactory
    {
        IResellerBalancePage IResellerBalancePageFactory.Create(IWebDriver driver, SoftwareTypeEnum softwareType)
        {
            switch (softwareType)
            {
                case SoftwareTypeEnum.riverSweeps:
                    return new RiverSweepsResellerBalancePage(driver);
                case SoftwareTypeEnum.ultraMonster:
                    return new UltraMonsterResellerBalancePage(driver);
                case SoftwareTypeEnum.vPower:
                    return new VPowerResellerBalancePage(driver);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXResellerBalancePage(driver);
                case SoftwareTypeEnum.grandX:
                    return new GrandXResellerBalancePage(driver);
                case SoftwareTypeEnum.gaminator:
                    return new GaminatorG3ResellerBalancePage(driver);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarResellerBalancePage(driver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
