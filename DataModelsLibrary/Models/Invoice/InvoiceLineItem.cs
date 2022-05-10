namespace DataModelsLibrary
{
    public class InvoiceLineItem : BaseOrganizationEntity
    {
        public int InvoiceId { get; set; }
        public string ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}