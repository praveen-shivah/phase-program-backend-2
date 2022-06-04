namespace AuthenticationRepositoryTypes
{
    public class AuthenticationResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
