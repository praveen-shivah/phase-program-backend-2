namespace AutomaticTaskSharedLibrary
{
    public class ResellerBalanceRetrieveRequest
    {
        public string OrganizationId { get; set; }
        public string ApiKey { get; set; }
        public int ResellerId { get; set; }
        public SoftwareType SoftwareType { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
