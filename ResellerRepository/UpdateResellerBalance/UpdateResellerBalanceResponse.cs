namespace ResellerRepository;

using APISupportTypes;

using DatabaseContext;

public class UpdateResellerBalanceResponse : BaseResponseDto
{
    public Reseller? Reseller { get; set; }
}