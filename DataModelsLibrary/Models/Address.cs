namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(CityId), Name = "IX_Address_CityId")]
[Index(nameof(ContactId), Name = "IX_Address_ContactId")]
[Index(nameof(OrganizationId), Name = "IX_Address_OrganizationId")]
[Index(nameof(PostalCodeId), Name = "IX_Address_PostalCodeId")]
[Index(nameof(StateProvinceId), Name = "IX_Address_StateProvinceId")]
    public partial class Address : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string AddressLine2 { get; set; } = null!;
        public int CityId { get; set; }
        public int StateProvinceId { get; set; }
        public int PostalCodeId { get; set; }
        public int? ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Address")]
        public virtual City City { get; set; } = null!;
        [ForeignKey(nameof(ContactId))]
        [InverseProperty("Address")]
        public virtual Contact? Contact { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Address")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(PostalCodeId))]
        [InverseProperty("Address")]
        public virtual PostalCode PostalCode { get; set; } = null!;
        [ForeignKey(nameof(StateProvinceId))]
        [InverseProperty("Address")]
        public virtual StateProvince StateProvince { get; set; } = null!;
    }
}
