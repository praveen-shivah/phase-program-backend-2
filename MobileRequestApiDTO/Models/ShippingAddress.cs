using Newtonsoft.Json; 
namespace MobileRequestApiDTO{ 

    public class ShippingAddress
    {
        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("attention")]
        public string Attention { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }
    }

}