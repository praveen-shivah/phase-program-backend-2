namespace DataModelsLibrary
{
    public class InvoiceLineItem : BaseOrganizationEntity
    {
        public DateTime? DateTimeProcessStarted { get; set; }

        public DateTime? DateTimeSent { get; set; }

        public string Description { get; set; }

        public int InvoiceId { get; set; }

        public string ItemId { get; set; }

        public int Quantity { get; set; }

        public string SoftwareType { get; set; }
    }
}