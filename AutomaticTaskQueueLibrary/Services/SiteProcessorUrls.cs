using Microsoft.Extensions.Configuration;

namespace AutomaticTaskQueueLibrary;

public class SiteProcessorUrls : ISiteProcessorUrls
{
    private readonly IConfiguration configuration;

    public SiteProcessorUrls(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    string ISiteProcessorUrls.GetTransferPointsUrl()
    {
        return $"{this.configuration.GetSection("SiteUrls:ProcessUrl").Value}/transfer-points";
    }

    string ISiteProcessorUrls.GetRetrieveBalanceUrl()
    {
        return $"{this.configuration.GetSection("SiteUrls:ProcessUrl").Value}/retrieve-balance";
    }
}