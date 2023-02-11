namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(InvoiceId), Name = "IX_InvoiceLineItem_InvoiceId")]
[Index(nameof(OrganizationId), Name = "IX_InvoiceLineItem_OrganizationId")]
    public partial class InvoiceLineItem : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ItemId { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public string SoftwareType { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }
        public DateTime? DateTimeProcessStarted { get; set; }
        public DateTime? DateTimeSent { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty("InvoiceLineItem")]
        public virtual Invoice Invoice { get; set; } = null!;
        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("InvoiceLineItem")]
        public virtual Organization Organization { get; set; } = null!;
    }
}
