namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;
    public class ResellerPlayersRetrieveRequest
    {
        public ResellerPlayersRetrieveRequest(
            SoftwareTypeEnum softwareType,
            int organizationId,
            string apiKey,
            int resellerId,
            string siteUserId,
            string sitePassword,
            int vendorId,
            int drawer)
        {
            this.SoftwareType = softwareType;
            this.OrganizationId = organizationId;
            this.ApiKey = apiKey;
            this.ResellerId = resellerId;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
            this.LoginPageInformation.LoginPage = this.ApiKey;
            this.VendorId = vendorId;
            this.Drawer = drawer;
            this.LoginPageInformation.Drawer = drawer;
        }

        public LoginPageInformation LoginPageInformation { get; }

        public SoftwareTypeEnum SoftwareType { get; }

        public int OrganizationId { get; }

        public string ApiKey { get; }

        public int ResellerId { get; }
        public int VendorId { get; }
        public int Drawer { get; set; }
    }
}
