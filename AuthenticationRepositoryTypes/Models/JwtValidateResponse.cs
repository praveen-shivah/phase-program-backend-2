namespace AuthenticationRepositoryTypes
{
    using System.IdentityModel.Tokens.Jwt;

    public class JwtValidateResponse
    {
        public bool IsSuccessful { get; set; }

        public bool IsExpired { get; set; }

        public JwtSecurityToken? JwtSecurityToken { get; set; }
    }
}
