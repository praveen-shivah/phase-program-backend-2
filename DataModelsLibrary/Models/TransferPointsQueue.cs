namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_TransferPointsQueue_OrganizationId")]
    public partial class TransferPointsQueue : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string AccountId { get; set; } = null!;
        [Column("APIKey")]
        public string Apikey { get; set; } = null!;
        public int InvoiceLineItemId { get; set; }
        public string Password { get; set; } = null!;
        public int Points { get; set; }
        public int SoftwareType { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime? DateTimeProcessStarted { get; set; }
        public DateTime? DateTimeSent { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }
        public string ItemId { get; set; } = null!;

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("TransferPointsQueue")]
        public virtual Organization Organization { get; set; } = null!;
    }
}
