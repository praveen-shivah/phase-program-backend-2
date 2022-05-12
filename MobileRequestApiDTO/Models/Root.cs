using Newtonsoft.Json; 
namespace ApiDTO
{ 

    public class Root
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }

}