namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_Country_OrganizationId")]
    public partial class Country : BaseEntity
    {
        public Country()
        {
            PostalCode = new HashSet<PostalCode>();
            StateProvince = new HashSet<StateProvince>();
        }

        [Key]
        public int Id { get; set; }
        public string CountryId { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Country")]
        public virtual Organization Organization { get; set; } = null!;
        [InverseProperty("Country")]
        public virtual ICollection<PostalCode> PostalCode { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<StateProvince> StateProvince { get; set; }
    }
}
