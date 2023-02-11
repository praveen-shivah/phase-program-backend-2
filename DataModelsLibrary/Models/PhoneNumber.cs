namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(ContactId), Name = "IX_PhoneNumber_ContactId")]
[Index(nameof(OrganizationId), Name = "IX_PhoneNumber_OrganizationId")]
    public partial class PhoneNumber : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int? ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("PhoneNumber")]
        public virtual Contact? Contact { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("PhoneNumber")]
        public virtual Organization Organization { get; set; } = null!;
    }
}
