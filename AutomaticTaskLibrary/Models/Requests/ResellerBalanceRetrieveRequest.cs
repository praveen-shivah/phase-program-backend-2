namespace AutomaticTaskLibrary
{
    public class ResellerBalanceRetrieveRequest
    {
        public int ResellerId { get; set; }
        public SoftwareType SoftwareType { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
