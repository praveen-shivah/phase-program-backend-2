namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class Invoice : BaseEntity
    {
        public int Balance { get; set; }

        public string BalanceFormatted { get; set; }

        public string CfCustomerType { get; set; }

        public string CfCustomerTypeUnformatted { get; set; }

        public string CfSiteNumber { get; set; }

        public string CfSiteNumberUnformatted { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedDateFormatted { get; set; }

        public DateTime CreatedTime { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceUrl { get; set; }

        public List<LineItem> LineItems { get; set; }

        public string Status { get; set; }

        public string StatusFormatted { get; set; }
    }
}