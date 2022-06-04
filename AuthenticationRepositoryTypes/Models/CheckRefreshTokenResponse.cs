namespace AuthenticationRepositoryTypes
{
    public class CheckRefreshTokenResponse
    {
        public bool IsSuccessful { get; set; }

        public CheckRefreshTokenResponseType CheckRefreshTokenResponseType { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
