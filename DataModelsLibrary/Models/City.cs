namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_City_OrganizationId")]
    public partial class City : BaseEntity
    {
        public City()
        {
            Address = new HashSet<Address>();
        }

        [Key]
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("City")]
        public virtual Organization Organization { get; set; } = null!;
        [InverseProperty("City")]
        public virtual ICollection<Address> Address { get; set; }
    }
}
