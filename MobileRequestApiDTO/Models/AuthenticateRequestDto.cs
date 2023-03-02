namespace ApiDTO
{
    public class AuthenticateRequestDto
    {
        public string user { get; set; } = string.Empty;

        public string pwd { get; set; } = string.Empty;

        public string audience { get; set; } = string.Empty;
    }
}
