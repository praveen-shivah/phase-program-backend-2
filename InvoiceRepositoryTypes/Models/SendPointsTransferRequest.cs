namespace InvoiceRepositoryTypes
{
    public class SendPointsTransferRequest
    {
        public int SiteId { get; set; }
        public int AccountId { get; set; }
        public int Points { get; set; }
    }
}
