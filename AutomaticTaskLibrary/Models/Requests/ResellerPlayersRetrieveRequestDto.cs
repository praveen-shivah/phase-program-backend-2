namespace AutomaticTaskSharedLibrary
{
    using ApiDTO;

    public class ResellerPlayersRetrieveRequestDto
    {
        public int OrganizationId { get; set; }
        public int ResellerId { get; set; }
        public int VendorId { get; set; }
        public SoftwareTypeEnum SoftwareType { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public int Drawer { get; set; }
    }
}
