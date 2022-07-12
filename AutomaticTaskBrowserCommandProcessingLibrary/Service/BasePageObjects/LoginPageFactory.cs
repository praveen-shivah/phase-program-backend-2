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
                default:
                    throw new ArgumentOutOfRangeException(nameof(loginPageInformation.SoftwareType), loginPageInformation.SoftwareType, null);
            }
        }
    }
}