namespace ResellerRepository;

public class UpdateResellerSiteRequest
{
    public int OrganizationId { get; set; }

    public bool IgnoreResellerId { get; set; }

    public int ResellerId { get; set; }

    public int Id { get; set; }

    public string AccountId { get; set; } = string.Empty;

    public string LoginUsername { get; set; } = string.Empty;

    public string LoginPassword { get; set; } = string.Empty;
}