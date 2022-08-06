namespace DataModelsLibrary
{
    public class InvoiceRevision : BaseOrganizationEntity
    {
        public int ResellerId { get; set; }
        public int InvoiceId { get; set; } 
        public string Invoice_Id { get; set; }
        public string Json { get; set; }
    }
}