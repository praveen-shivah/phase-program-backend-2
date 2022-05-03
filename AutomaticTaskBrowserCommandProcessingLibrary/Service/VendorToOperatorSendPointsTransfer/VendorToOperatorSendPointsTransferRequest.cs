namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public class VendorToOperatorSendPointsTransferRequest
    {
        public VendorToOperatorSendPointsTransferRequest(string siteUserId,
                                                         string sitePassword,
                                                         string destinationAccountId,
                                                         int points)
        {
            this.SiteUserId = siteUserId;
            this.SitePassword = sitePassword;
            this.DestinationAccountId = destinationAccountId;
            this.Points = points;
        }

        public string DestinationAccountId { get; }

        public int Points { get; }

        public string SitePassword { get; }

        public string SiteUserId { get; }
    }
}
