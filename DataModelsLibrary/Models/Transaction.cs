namespace DatabaseContext
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Transaction
    {
        [Key] 
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public string Time { get; set; }
        public string Station { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Comps { get; set; }
        public string Free { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }
        public int ResellerId { get; set; }
        public int VendorId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("Transaction")]
        public virtual Organization Organization { get; set; } = null!;
        [ForeignKey(nameof(ResellerId))]
        [InverseProperty("Transaction")]
        public virtual Reseller Reseller { get; set; } = null!;
        [ForeignKey(nameof(VendorId))]
        [InverseProperty("Transaction")]
        public virtual Vendor Vendor { get; set; } = null!;
    }
}
