namespace AuthenticationRepository
{
    public class ISAccountUpdateRequestDto
    {
        public int OrganizationId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Claims { get; set; } = string.Empty;
    }
}