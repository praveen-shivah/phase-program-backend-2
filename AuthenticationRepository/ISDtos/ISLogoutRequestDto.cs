namespace AuthenticationRepository
{
    public class ISLogoutRequestDto
    {
        public int OrganizationId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;
    }
}