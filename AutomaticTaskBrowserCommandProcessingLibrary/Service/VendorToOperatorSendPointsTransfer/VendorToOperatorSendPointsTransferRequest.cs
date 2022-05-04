namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskLibrary;

    public class VendorToOperatorSendPointsTransferRequest
    {
        public VendorToOperatorSendPointsTransferRequest(
            SoftwareType softwareType,
            string siteUserId,
            string sitePassword,
            string destinationAccountId,
            int points)
        {
            this.SoftwareType = softwareType;
            this.SiteUserId = siteUserId;
            this.SitePassword = sitePassword;
            this.DestinationAccountId = destinationAccountId;
            this.Points = points;
        }

        public string DestinationAccountId { get; }

        public int Points { get; }

        public string SitePassword { get; }

        public string SiteUserId { get; }

        public SoftwareType SoftwareType { get; }
    }
}