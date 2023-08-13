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
                case SoftwareTypeEnum.ultraPanda:
                    return new UltraPandaResellerBalancePage(driver);
                case SoftwareTypeEnum.vBlink:
                    return new VBlinkResellerBalancePage(driver);
                case SoftwareTypeEnum.vegasX:
                    return new VegasXResellerBalancePage(driver);
                case SoftwareTypeEnum.grandX:
                    return new GrandXResellerBalancePage(driver);
                case SoftwareTypeEnum.gaminator:
                    return new GaminatorG3ResellerBalancePage(driver);
                case SoftwareTypeEnum.pampazar:
                    return new PampazarResellerBalancePage(driver);
                case SoftwareTypeEnum.goldenBuffalo:
                    return new GoldenBuffaloResellerBalancePage(driver);
                case SoftwareTypeEnum.goldenDragon:
                    return new GoldenDragonResellerBalancePage(driver);
                case SoftwareTypeEnum.poseidonX:
                    return new PoseidonXResellerBalancePage(driver);
                case SoftwareTypeEnum.bcLive:
                    return new BCLiveResellerBalancePage(driver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
