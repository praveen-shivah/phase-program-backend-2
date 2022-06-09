namespace AuthenticationRepositoryTypes
{
    public class AuthenticationRequest
    {
        public AuthenticationRequest(string userId, string password, string ipAddress)
        {
            this.UserId = userId;
            this.Password = password;
            this.IpAddress = ipAddress;
        }

        public string Password { get; }

        public string IpAddress { get; }

        public string UserId { get; }
    }
}