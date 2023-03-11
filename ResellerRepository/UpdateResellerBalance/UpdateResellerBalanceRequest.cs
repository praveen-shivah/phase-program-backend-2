namespace ResellerRepository;

public class UpdateResellerBalanceRequest
{
    public int OrganizationId { get; set; }

    public int ResellerId { get; set; }

    public int Balance { get; set; }
}