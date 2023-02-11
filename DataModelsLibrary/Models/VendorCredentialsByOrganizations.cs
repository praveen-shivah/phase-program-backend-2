namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_VendorCredentialsByOrganizations_OrganizationId")]
[Index(nameof(VendorId), Name = "IX_VendorCredentialsByOrganizations_VendorId")]
    public partial class VendorCredentialsByOrganizations : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("VendorCredentialsByOrganizations")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("VendorCredentialsByOrganizations")]
        public virtual Vendor Vendor { get; set; } = null!;
    }
}
