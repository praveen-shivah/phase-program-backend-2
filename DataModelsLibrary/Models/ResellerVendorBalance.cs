namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_ResellerVendorBalance_OrganizationId")]
[Index(nameof(ResellerId), Name = "IX_ResellerVendorBalance_ResellerId")]
[Index(nameof(VendorId), Name = "IX_ResellerVendorBalance_VendorId")]
    public partial class ResellerVendorBalance : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ResellerId { get; set; }
        public int Balance { get; set; }
        public DateTime LastUpdated { get; set; }
        public int VendorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("ResellerVendorBalance")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(ResellerId))]
        [InverseProperty("ResellerVendorBalance")]
        public virtual Reseller Reseller { get; set; } = null!;
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("ResellerVendorBalance")]
        public virtual Vendor Vendor { get; set; } = null!;
    }
}
