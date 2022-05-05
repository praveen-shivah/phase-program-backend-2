namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    using OpenQA.Selenium;

    public class LoginPageFactory : ILoginPageFactory
    {
        ILoginPage ILoginPageFactory.Create(IWebDriver webDriver, LoginPageInformation loginPageInformation)
        {
            switch (loginPageInformation.SoftwareType)
            {
                case SoftwareType.riverSweeps:
                    return new RiverSweepsVendorToOperatorTransferLogin(webDriver, loginPageInformation);
                default:
                    throw new ArgumentOutOfRangeException(nameof(loginPageInformation.SoftwareType), loginPageInformation.SoftwareType, null);
            }
        }
    }
}