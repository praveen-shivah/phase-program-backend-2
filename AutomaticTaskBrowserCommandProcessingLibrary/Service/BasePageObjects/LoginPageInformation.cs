namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public class LoginPageInformation
    {
        public LoginPageInformation(
            SoftwareType softwareType,
            string siteUserId,
            string sitePassword)
        {
            this.SoftwareType = softwareType;
            this.SiteUserId = siteUserId;
            this.SitePassword = sitePassword;
        }

        public string SitePassword { get; }

        public string SiteUserId { get; }

        public SoftwareType SoftwareType { get; }
    }
}