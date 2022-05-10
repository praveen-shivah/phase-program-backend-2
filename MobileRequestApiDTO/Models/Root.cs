using Newtonsoft.Json; 
namespace MobileRequestApiDTO{ 

    public class Root
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }

}