namespace APISupport
{
    using System.ComponentModel;

    public class ISAccountRequestDto
    {
        [DefaultValue(1)]
        public int OrganizationId { get; set; }
        [DefaultValue("carl")]
        public string UserName { get; set; } = string.Empty;
    }
}