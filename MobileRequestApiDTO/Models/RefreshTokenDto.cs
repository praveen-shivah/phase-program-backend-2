namespace ApiDTO
{
    public class RefreshTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; } = string.Empty;
    }
}
