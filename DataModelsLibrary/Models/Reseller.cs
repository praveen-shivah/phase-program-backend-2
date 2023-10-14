namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_Reseller_OrganizationId")]
    public partial class Reseller : BaseEntity
    {
        public Reseller()
        {
            Contact = new HashSet<Contact>();
            Invoice = new HashSet<Invoice>();
            SiteInformation = new HashSet<SiteInformation>();
            Players = new HashSet<Players>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }
        public int Balance { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Reseller")]
        public virtual Organization Organization { get; set; } = null!;
        [InverseProperty("Reseller")]
        public virtual ICollection<Contact> Contact { get; set; }
        [InverseProperty("Reseller")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        [InverseProperty("Reseller")]
        public virtual ICollection<SiteInformation> SiteInformation { get; set; }
        [InverseProperty("Reseller")]
        public virtual ICollection<Players> Players { get; set; }
    }
}
