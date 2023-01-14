namespace AutomaticTaskSharedLibrary
{
    using ApiDTO;

    public class DistributorToResellerSendPointsTransferRequestDto
    {
        public int InvoiceLineItemId { get; set; }
        public int OrganizationId { get; set; }
        public string APIKey { get; set; }
        public SoftwareTypeEnum SoftwareType { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public int Points { get; set; }
    }
}
