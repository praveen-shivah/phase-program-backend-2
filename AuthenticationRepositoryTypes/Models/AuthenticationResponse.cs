namespace AuthenticationRepositoryTypes
{
    using RestServicesSupportTypes;

    public class AuthenticationResponse : BaseResponseDto
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<int> Roles { get; set; }
    }
}
