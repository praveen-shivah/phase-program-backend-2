namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_Contact_OrganizationId")]
[Index(nameof(ResellerId), Name = "IX_Contact_ResellerId")]
    public partial class Contact : BaseEntity
    {
        public Contact()
        {
            Address = new HashSet<Address>();
            PhoneNumber = new HashSet<PhoneNumber>();
        }

        [Key]
        public int Id { get; set; }
        public string First { get; set; } = null!;
        public string Last { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public int? ResellerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Contact")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(ResellerId))]
        [InverseProperty("Contact")]
        public virtual Reseller? Reseller { get; set; }
        [InverseProperty("Contact")]
        public virtual ICollection<Address> Address { get; set; }
        [InverseProperty("Contact")]
        public virtual ICollection<PhoneNumber> PhoneNumber { get; set; }
    }
}
