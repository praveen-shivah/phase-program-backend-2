namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;


[Index(nameof(SoftwareTypeId), Name = "IX_Vendor_SoftwareTypeId")]
    public partial class Vendor : BaseEntity
    {
        public Vendor()
        {
            SiteInformation = new HashSet<SiteInformation>();
            VendorCredentialsByOrganizations = new HashSet<VendorCredentialsByOrganizations>();
            Players = new HashSet<Players>();
            Transaction = new HashSet<Transaction>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SoftwareTypeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        [ForeignKey(nameof(SoftwareTypeId))]
        [InverseProperty("Vendor")]
        public virtual SoftwareType SoftwareType { get; set; } = null!;
        [InverseProperty("Vendor")]
        public virtual ICollection<SiteInformation> SiteInformation { get; set; }
        [InverseProperty("Vendor")]
        public virtual ICollection<VendorCredentialsByOrganizations> VendorCredentialsByOrganizations { get; set; }
        [InverseProperty("Vendor")]
        public virtual ICollection<Players> Players { get; set; }
        [InverseProperty("Vendor")]
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
