namespace AuthenticationRepositoryTypes
{
    public class AuthenticationRequest
    {
        public AuthenticationRequest(string userId, string password)
        {
            this.UserId = userId;
            this.Password = password;
        }

        public string Password { get; }

        public string UserId { get; }
    }
}