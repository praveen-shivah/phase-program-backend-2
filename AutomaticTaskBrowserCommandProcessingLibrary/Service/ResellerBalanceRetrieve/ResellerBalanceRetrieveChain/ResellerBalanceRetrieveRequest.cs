namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public class ResellerBalanceRetrieveRequest
    {
        public ResellerBalanceRetrieveRequest(
            SoftwareType softwareType,
            int resellerId,
            string siteUserId,
            string sitePassword)
        {
            this.SoftwareType = softwareType;
            this.ResellerId = resellerId;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
        }

        public LoginPageInformation LoginPageInformation { get; }

        public SoftwareType SoftwareType { get; }

        public int ResellerId { get; }
    }
}