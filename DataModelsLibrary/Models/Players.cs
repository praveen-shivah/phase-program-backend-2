namespace DatabaseContext
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Players: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ResellerId { get; set; }
        public int VendorId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public int Balance { get; set; }
        public string PlayerId { get; set; }
        public string MobileId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Players")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(ResellerId))]
        [InverseProperty("Players")]
        public virtual Reseller Reseller { get; set; } = null!;
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("Players")]
        public virtual Vendor Vendor { get; set; } = null!;

    }
}
