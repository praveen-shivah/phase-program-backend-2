namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using ApiDTO;

    public class DistributorToResellerSendPointsTransferRequest
    {
        public DistributorToResellerSendPointsTransferRequest(
            int invoiceLineItemId,
            SoftwareTypeEnum softwareType,
            string siteUserId,
            string sitePassword,
            string destinationAccountId,
            int points)
        {
            this.InvoiceLineItemId = invoiceLineItemId;
            this.SoftwareType = softwareType;
            this.LoginPageInformation = new LoginPageInformation(softwareType, siteUserId, sitePassword);
            this.DestinationAccountId = destinationAccountId;
            this.Points = points;
        }

        public string DestinationAccountId { get; }

        public int InvoiceLineItemId { get; }

        public LoginPageInformation LoginPageInformation { get; }

        public int Points { get; }

        public SoftwareTypeEnum SoftwareType { get; }
    }
}