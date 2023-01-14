namespace AutomaticTaskSharedLibrary
{
    using ApiDTO;

    public class ResellerBalanceRetrieveRequestDto
    {
        public string OrganizationId { get; set; }
        public string ApiKey { get; set; }
        public int ResellerId { get; set; }
        public SoftwareTypeEnum SoftwareType { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
