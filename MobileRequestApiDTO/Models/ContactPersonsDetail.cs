using Newtonsoft.Json; 
namespace MobileRequestApiDTO{ 

    public class ContactPersonsDetail
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("contact_person_id")]
        public string ContactPersonId { get; set; }

        [JsonProperty("is_primary_contact")]
        public bool IsPrimaryContact { get; set; }

        [JsonProperty("photo_url")]
        public string PhotoUrl { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

}