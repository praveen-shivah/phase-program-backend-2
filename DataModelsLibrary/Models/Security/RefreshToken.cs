namespace DataModelsLibrary
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    public class RefreshToken
    {
        public DateTime Created { get; set; }

        public string CreatedByIp { get; set; }

        public DateTime Expires { get; set; }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public bool IsActive => !this.IsRevoked && !this.IsExpired;

        public bool IsExpired => DateTime.UtcNow >= this.Expires;

        public bool IsRevoked => this.Revoked != null;

        public string ReasonRevoked { get; set; } = string.Empty;

        public string ReplacedByToken { get; set; } = string.Empty;

        public DateTime? Revoked { get; set; }

        public string RevokedByIp { get; set; } = string.Empty;

        public string Token { get; set; }
    }
}