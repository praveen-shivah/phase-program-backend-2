namespace ApiDTO
{
    public class ResellerBalanceDTO : CallBackInformationDTO
    {
        public int ResellerId { get; set; }

        public int SiteInformationId { get; set; }

        public string Balance { get; set; }
    }
}
