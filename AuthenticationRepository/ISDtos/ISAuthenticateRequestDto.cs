namespace AuthenticationRepository
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ISAuthenticateRequestDto
    {
        [DefaultValue(1)]
        [Required]
        public int OrganizationId { get; set; }

        [DefaultValue("primepay@primerogames.com")]
        [Required]
        public string User { get; set; }

        [DefaultValue("password")]
        [Required]
        public string Password { get; set; }

        [DefaultValue("")]
        [Required]
        public string Audience { get; set; }

        [DefaultValue("")]
        [Required]
        public string Issuer { get; set; }

        [DefaultValue("107.222.175.60, 172.31.18.54")]
        [Required]
        public string IPAddress { get; set; }
    }
}
