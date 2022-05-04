namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public class VendorBalanceRetrieveRequest
    {
        public VendorBalanceRetrieveRequest(
            SoftwareType softwareType,
            string siteUserId,
            string sitePassword)
        {
            this.SoftwareType = softwareType;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
        }

        public LoginPageInformation LoginPageInformation { get; }

        public SoftwareType SoftwareType { get; }
    }
}