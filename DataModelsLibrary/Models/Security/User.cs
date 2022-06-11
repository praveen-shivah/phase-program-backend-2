namespace DataModelsLibrary
{
    using Newtonsoft.Json;

    public class User : BaseOrganizationEntity
    {
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string CurrentRefreshToken { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
