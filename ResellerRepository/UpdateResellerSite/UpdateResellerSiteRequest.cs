namespace ResellerRepository;

public class UpdateResellerSiteRequest
{
    public int OrganizationId { get; set; }

    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

}