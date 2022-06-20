namespace ApiHost
{
    public class JwtValidateResponse
    {
        public bool IsSuccessful { get; set; }

        public int UserId { get; set; }

        public int OrganizationId { get; set; }
    }
}
