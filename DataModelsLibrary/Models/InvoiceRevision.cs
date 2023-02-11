namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(InvoiceId), Name = "IX_InvoiceRevision_InvoiceId")]
[Index(nameof(OrganizationId), Name = "IX_InvoiceRevision_OrganizationId")]
    public partial class InvoiceRevision : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        public int InvoiceId { get; set; }
        [Column("Invoice_Id")]
        public string InvoiceId1 { get; set; } = null!;
        public string Json { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty("InvoiceRevision")]
        public virtual Invoice Invoice { get; set; } = null!;
        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("InvoiceRevision")]
        public virtual Organization Organization { get; set; } = null!;
    }
}
