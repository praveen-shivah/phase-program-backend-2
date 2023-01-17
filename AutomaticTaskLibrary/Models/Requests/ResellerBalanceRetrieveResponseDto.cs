namespace InvoiceRepositoryTypes
{
    using RestServicesSupportTypes;

    public class ResellerBalanceRetrieveResponseDto : BaseResponseDto
    {
        public int BalanceAsPoints { get; set; }
    }
}
