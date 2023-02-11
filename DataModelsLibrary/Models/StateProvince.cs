namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(CountryId), Name = "IX_StateProvince_CountryId")]
[Index(nameof(OrganizationId), Name = "IX_StateProvince_OrganizationId")]
    public partial class StateProvince : BaseEntity
    {
        public StateProvince()
        {
            Address = new HashSet<Address>();
        }

        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("StateProvince")]
        public virtual Country Country { get; set; } = null!;
        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("StateProvince")]
        public virtual Organization Organization { get; set; } = null!;
        [InverseProperty("StateProvince")]
        public virtual ICollection<Address> Address { get; set; }
    }
}
