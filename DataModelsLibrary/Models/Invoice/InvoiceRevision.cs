namespace DataModelsLibrary
{
    using DataSharedLibrary;

    using Microsoft.EntityFrameworkCore;

    [Index(nameof(OrganizationId), nameof(Invoice_Id), Name = "IX_InvoiceRevisionInvoice_Id")]
    public class InvoiceRevision : BaseEntity
    {
        public string OrganizationId { get; set; }
        public int InvoiceId { get; set; } 
        public string Invoice_Id { get; set; }
        public string Json { get; set; }
    }
}