namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    public class ResellerBalanceRetrieveRequest
    {
        public ResellerBalanceRetrieveRequest(
            SoftwareType softwareType,
            string organizationId,
            string apiKey,
            int resellerId,
            string siteUserId,
            string sitePassword)
        {
            this.SoftwareType = softwareType;
            this.OrganizationId = organizationId;
            this.ApiKey = apiKey;
            this.ResellerId = resellerId;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
        }

        public LoginPageInformation LoginPageInformation { get; }

        public SoftwareType SoftwareType { get; }

        public string OrganizationId { get; }

        public string ApiKey { get; }

        public int ResellerId { get; }
    }
}