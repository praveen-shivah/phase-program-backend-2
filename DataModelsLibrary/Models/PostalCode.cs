namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(CountryId), Name = "IX_PostalCode_CountryId")]
[Index(nameof(OrganizationId), Name = "IX_PostalCode_OrganizationId")]
    public partial class PostalCode : BaseEntity
    {
        public PostalCode()
        {
            Address = new HashSet<Address>();
        }

        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("PostalCode")]
        public virtual Country Country { get; set; } = null!;
        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("PostalCode")]
        public virtual Organization Organization { get; set; } = null!;
        [InverseProperty("PostalCode")]
        public virtual ICollection<Address> Address { get; set; }
    }
}
