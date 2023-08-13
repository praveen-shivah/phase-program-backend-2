namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    public class LoginPageInformation
    {
        public LoginPageInformation(
            SoftwareTypeEnum softwareType,
            string siteUserId,
            string sitePassword)
        {
            this.SoftwareType = softwareType;
            this.SiteUserId = siteUserId;
            this.SitePassword = sitePassword;
        }

        public string SitePassword { get; }

        public string SiteUserId { get; }

        public SoftwareTypeEnum SoftwareType { get; }
        public string LoginPage { get; set; }
    }
}