namespace AuthenticationRepository
{
    using DataModelsLibrary;

    public class AuthenticateUserResponse
    {
        public bool IsSuccessful { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string JwtToken { get; set; }
        public List<int> Roles { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
