using Newtonsoft.Json;
namespace ApiDTO
{
    public class PaymentOptions
    {
        [JsonProperty("payment_gateways")]
        public List<object> PaymentGateways { get; set; }
    }

}