namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(UserId), Name = "IX_RefreshToken_UserId")]
    public partial class RefreshToken : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedByIp { get; set; }
        public DateTime Expires { get; set; }
        public string? ReasonRevoked { get; set; }
        public string? ReplacedByToken { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? Token { get; set; }
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("RefreshToken")]
        public virtual User? User { get; set; }
    }
}
