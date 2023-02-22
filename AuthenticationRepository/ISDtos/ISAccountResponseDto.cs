namespace APISupport
{
    using RestServicesSupportTypes;

    public class ISAccountResponseDto : BaseResponseDto
    {
        public int OrganizationId { get; set; }

        public string UserName { get; set; }

        public string Claims { get; set; }
    }
}
