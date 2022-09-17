namespace DataModelsLibrary
{
    using ApiDTO;

    public class TransferPointsQueue : BaseOrganizationEntity
    {
        public string AccountId { get; set; }

        public string APIKey { get; set; }

        public int InvoiceLineItemId { get; set; }

        public string Password { get; set; }

        public int Points { get; set; }

        public SoftwareTypeEnum SoftwareType { get; set; }

        public string UserId { get; set; }

        public DateTime? DateTimeProcessStarted { get; set; }

        public DateTime? DateTimeSent { get; set; }
    }
}