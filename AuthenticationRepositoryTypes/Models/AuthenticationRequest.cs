namespace AuthenticationRepositoryTypes
{
    public class AuthenticationRequest
    {
        public AuthenticationRequest(int organizationId, string userId, string password, string ipAddress, string audience)
        {
            this.OrganizationId = organizationId;
            this.UserId = userId;
            this.Password = password;
            this.IpAddress = ipAddress;
            this.Audience = audience;
        }

        public string Password { get; }

        public string IpAddress { get; }

        public string Audience { get; }

        public int OrganizationId { get; }

        public string UserId { get; }
    }
}