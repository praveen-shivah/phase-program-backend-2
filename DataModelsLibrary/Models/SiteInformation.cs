namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_SiteInformation_OrganizationId")]
[Index(nameof(ResellerId), Name = "IX_SiteInformation_ResellerId")]
[Index(nameof(VendorId), Name = "IX_SiteInformation_VendorId")]
    public partial class SiteInformation : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Column("Item_Id")]
        public string ItemId { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Column("URL")]
        public string Url { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public int ResellerId { get; set; }
        public int VendorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("SiteInformation")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(ResellerId))]
        [InverseProperty("SiteInformation")]
        public virtual Reseller Reseller { get; set; } = null!;
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("SiteInformation")]
        public virtual Vendor Vendor { get; set; } = null!;
    }
}
