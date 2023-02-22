namespace AuthenticationRepository
{
    public class AuthenticateUserRequest
    {
        public int OrganizationId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
        public string Audience { get; set; }
    }
}
