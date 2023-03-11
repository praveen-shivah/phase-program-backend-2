namespace ResellerRepository;

public class UpdateResellerSiteRequest
{
    public int OrganizationId { get; set; }

    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

    public string LoginUsername { get; set; } = string.Empty;

    public string LoginPassword { get; set; } = string.Empty;
}