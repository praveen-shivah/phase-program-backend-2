using Newtonsoft.Json; 
namespace ApiDTO
{ 

    public class ItemCustomField
    {
        [JsonProperty("customfield_id")]
        public string CustomfieldId { get; set; }

        [JsonProperty("show_in_store")]
        public bool ShowInStore { get; set; }

        [JsonProperty("show_in_portal")]
        public bool ShowInPortal { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("show_on_pdf")]
        public bool ShowOnPdf { get; set; }

        [JsonProperty("edit_on_portal")]
        public bool EditOnPortal { get; set; }

        [JsonProperty("edit_on_store")]
        public bool EditOnStore { get; set; }

        [JsonProperty("show_in_all_pdf")]
        public bool ShowInAllPdf { get; set; }

        [JsonProperty("value_formatted")]
        public string ValueFormatted { get; set; }

        [JsonProperty("search_entity")]
        public string SearchEntity { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_dependent_field")]
        public bool IsDependentField { get; set; }
    }

}