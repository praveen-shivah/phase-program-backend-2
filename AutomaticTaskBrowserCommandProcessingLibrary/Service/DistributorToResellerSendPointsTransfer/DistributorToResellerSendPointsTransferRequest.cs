namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    public class DistributorToResellerSendPointsTransferRequest
    {
        public DistributorToResellerSendPointsTransferRequest(
            SoftwareTypeEnum softwareType,
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

        public SoftwareTypeEnum SoftwareType { get; }
    }
}