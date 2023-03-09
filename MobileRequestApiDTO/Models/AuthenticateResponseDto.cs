namespace ApiDTO
{
    using RestServicesSupportTypes;

    public class AuthenticateResponseDto : BaseResponseDto
    {
        public int OrganizationId { get; set; }

        public bool IsAuthenticated { get; set; }

        public string AccessToken { get; set; }

        public int[] Roles { get; set; } = Array.Empty<int>();
    }
}
