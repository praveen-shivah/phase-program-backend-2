namespace AuthenticationRepository
{
    public class AuthenticateUserResponse
    {
        public bool IsSuccessful { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
