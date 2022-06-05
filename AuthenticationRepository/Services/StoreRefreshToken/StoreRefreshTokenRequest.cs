namespace AuthenticationRepository
{
    public class StoreRefreshTokenRequest
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        public int UserId { get; set; }
    }
}
