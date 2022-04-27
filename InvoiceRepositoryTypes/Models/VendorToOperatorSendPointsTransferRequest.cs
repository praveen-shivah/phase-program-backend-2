namespace InvoiceRepositoryTypes
{
    public class VendorToOperatorSendPointsTransferRequest
    {
        public int OrganizationId { get; set; }
        public int SiteId { get; set; }
        public int AccountId { get; set; }
        public int Points { get; set; }
    }
}
