namespace DataModelsLibrary
{
    public class Invoice : BaseOrganizationEntity
    {
        public DateTime? DateTimeSent { get; set; }

        public double Balance { get; set; }

        public string BalanceFormatted { get; set; }

        public Reseller Reseller { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedDateFormatted { get; set; }

        public DateTime CreatedTime { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceUrl { get; set; }

        public List<InvoiceLineItem>? LineItems { get; set; }

        public List<InvoiceRevision> InvoiceRevisions { get; set; }

        public string Status { get; set; }

        public string StatusFormatted { get; set; }
    }
}