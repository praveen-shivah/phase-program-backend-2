namespace DatabaseContext
{
    public partial class RefreshToken
    {
        public bool IsActive => !this.IsRevoked && !this.IsExpired;
        public bool IsExpired => DateTime.UtcNow >= this.Expires;
        public bool IsRevoked => this.Revoked != null;
    }
}
