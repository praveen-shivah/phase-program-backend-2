using Newtonsoft.Json; 
namespace MobileRequestApiDTO{ 

    public class CustomerCustomFieldHash
    {
        [JsonProperty("cf_customer_type_unformatted")]
        public string CfCustomerTypeUnformatted { get; set; }

        [JsonProperty("cf_customer_type")]
        public string CfCustomerType { get; set; }

        [JsonProperty("cf_site_number")]
        public string CfSiteNumber { get; set; }

        [JsonProperty("cf_site_number_unformatted")]
        public string CfSiteNumberUnformatted { get; set; }
    }

}