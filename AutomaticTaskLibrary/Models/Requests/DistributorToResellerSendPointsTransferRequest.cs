namespace AutomaticTaskSharedLibrary
{
    using ApiDTO;

    public class DistributorToResellerSendPointsTransferRequest
    {
        public SoftwareTypeEnum SoftwareType { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public int Points { get; set; }
    }
}
