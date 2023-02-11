namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(OrganizationId), Name = "IX_User_OrganizationId")]
    public partial class User : BaseEntity
    {
        public User()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public string CurrentRefreshToken { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        [InverseProperty("User")]
        public virtual Organization Organization { get; set; } = null!;
        [InverseProperty("User")]
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
