﻿namespace AutomaticTaskBrowserCommandProcessingLibrary
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}
