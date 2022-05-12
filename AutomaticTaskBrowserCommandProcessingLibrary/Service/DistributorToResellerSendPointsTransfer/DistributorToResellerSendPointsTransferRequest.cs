namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using AutomaticTaskSharedLibrary;

    public class DistributorToResellerSendPointsTransferRequest
    {
        public DistributorToResellerSendPointsTransferRequest(
            SoftwareType softwareType,
            string siteUserId,
            string sitePassword,
            string destinationAccountId,
            int points)
        {
            this.SoftwareType = softwareType;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
            this.DestinationAccountId = destinationAccountId;
            this.Points = points;
        }

        public string DestinationAccountId { get; }

        public LoginPageInformation LoginPageInformation { get; }

        public int Points { get; }

        public SoftwareType SoftwareType { get; }
    }
}