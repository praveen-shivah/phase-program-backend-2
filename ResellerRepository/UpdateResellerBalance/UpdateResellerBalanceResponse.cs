namespace ResellerRepository;

using DatabaseContext;

using RestServicesSupportTypes;

public class UpdateResellerBalanceResponse : BaseResponseDto
{
    public Reseller? Reseller { get; set; }
}