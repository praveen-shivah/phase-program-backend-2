namespace AuthenticationRepositoryTypes
{
    using ApiDTO;

    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsAuthenticated { get; set; }
        public string JwtToken { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
        public List<int> Roles { get; set; }
    }
}
