namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    using OpenQA.Selenium;

    public class ManagementPageFactory : IManagementPageFactory
    {
        IManagementPage IManagementPageFactory.Create(IWebDriver webDriver, SoftwareType softwareType)
        {
            switch (softwareType)
            {
                case SoftwareType.riverSweeps:
                    return new RiverSweepsShopsManagement(webDriver);
                default:
                    throw new ArgumentOutOfRangeException(nameof(softwareType), softwareType, null);
            }
        }
    }
}