namespace ApiDTO
{
    public class AuthenticateResponseDto
    {
        public int OrganizationId { get; set; }

        public bool IsAuthenticated { get; set; }

        public string accessToken { get; set; }

        public int[] roles { get; set; }
    }
}
