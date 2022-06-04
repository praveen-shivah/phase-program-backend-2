namespace ApiDTO
{
    public class AuthenticateResponseDto
    {
        public bool IsAuthenticated { get; set; }

        public string accessToken { get; set; }

        public int[] roles { get; set; }
    }
}
