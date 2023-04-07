namespace AuthenticationRepository
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ISAuthenticateRequestDto
    {
        [DefaultValue(1)]
        [Required]
        public int OrganizationId { get; set; }

        [DefaultValue("admin")]
        [Required]
        public string User { get; set; }

        [DefaultValue("admin")]
        [Required]
        public string Password { get; set; }

        [DefaultValue("1234")]
        [Required]
        public string Audience { get; set; }

        [DefaultValue("1234")]
        [Required]
        public string Issuer { get; set; }

        [DefaultValue("127.0.0.1")]
        [Required]
        public string IPAddress { get; set; }
    }
}
