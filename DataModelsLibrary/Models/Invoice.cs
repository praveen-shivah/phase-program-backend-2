namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_Invoice_OrganizationId")]
[Index(nameof(ResellerId), Name = "IX_Invoice_ResellerId")]
    public partial class Invoice : BaseEntity
    {
        public Invoice()
        {
            InvoiceLineItem = new HashSet<InvoiceLineItem>();
            InvoiceRevision = new HashSet<InvoiceRevision>();
        }

        [Key]
        public int Id { get; set; }
        public double Balance { get; set; }
        public string BalanceFormatted { get; set; } = null!;
        public int ResellerId { get; set; }
        public string CreatedDate { get; set; } = null!;
        public string CreatedDateFormatted { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public string CustomerId { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string InvoiceId { get; set; } = null!;
        public string InvoiceNumber { get; set; } = null!;
        public string InvoiceUrl { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string StatusFormatted { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Invoice")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(ResellerId))]
        [InverseProperty("Invoice")]
        public virtual Reseller Reseller { get; set; } = null!;
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceLineItem> InvoiceLineItem { get; set; }
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceRevision> InvoiceRevision { get; set; }
    }
}
