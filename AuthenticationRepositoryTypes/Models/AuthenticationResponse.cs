namespace AuthenticationRepositoryTypes
{
    using ApiDTO;

    public class AuthenticationResponse
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<int> Roles { get; set; }
    }
}
