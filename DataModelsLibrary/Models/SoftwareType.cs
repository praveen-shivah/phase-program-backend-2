namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


    public partial class SoftwareType : BaseEntity
    {
        public SoftwareType()
        {
            Vendor = new HashSet<Vendor>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [InverseProperty("SoftwareType")]
        public virtual ICollection<Vendor> Vendor { get; set; }
    }
}
