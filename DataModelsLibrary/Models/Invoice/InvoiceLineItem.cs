namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class InvoiceLineItem : BaseEntity
    {
        public int InvoiceId { get; set; }
        public string ItemId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}