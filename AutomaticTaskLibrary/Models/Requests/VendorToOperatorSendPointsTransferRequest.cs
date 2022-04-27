namespace AutomaticTaskLibrary
{
    public class VendorToOperatorSendPointsTransferRequest
    {
        public SoftwareType SoftwareType { get; set; }
        public string SiteUrl { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public int Points { get; set; }
    }
}
