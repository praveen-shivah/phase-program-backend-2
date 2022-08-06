namespace ApiDTO
{
    using Newtonsoft.Json;

    public class CustomerCustomFieldHash
    {
        [JsonProperty("cf_reseller_id")]
        public int CfResellerId { get; set; }
        [JsonProperty("cf_reseller_id_unformatted")]
        public int CfResellerIdUnformatted { get; set; }
    }
}