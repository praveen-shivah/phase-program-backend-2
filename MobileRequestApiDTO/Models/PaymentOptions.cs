using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace MobileRequestApiDTO{ 

    public class PaymentOptions
    {
        [JsonProperty("payment_gateways")]
        public List<object> PaymentGateways { get; set; }
    }

}