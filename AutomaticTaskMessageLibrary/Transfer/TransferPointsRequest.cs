namespace AutomaticTaskMessageLibrary
{
    public class TransferPointsRequest
    {
        public int SiteId { get; set; }
        public int OperatorId { get; set; }
        public int AccountId { get; set; }
        public int Amount { get; set; }
        public string SiteURL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
