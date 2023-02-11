namespace ApiDTO
{
    using Newtonsoft.Json;

    public class ItemCustomField
    {
        [JsonProperty("api_name")]
        public string ApiName { get; set; }
        [JsonProperty("customfield_id")]
        public string CustomfieldId { get; set; }
        [JsonProperty("data_type")]
        public string DataType { get; set; }
        [JsonProperty("edit_on_portal")]
        public bool EditOnPortal { get; set; }
        [JsonProperty("edit_on_store")]
        public bool EditOnStore { get; set; }
        [JsonProperty("field_id")]
        public string FieldId { get; set; }
        [JsonProperty("index")]
        public int Index { get; set; }
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        [JsonProperty("is_dependent_field")]
        public bool IsDependentField { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }
        [JsonProperty("search_entity")]
        public string SearchEntity { get; set; }
        [JsonProperty("selected_option_id")]
        public string SelectedOptionId { get; set; }
        [JsonProperty("show_in_all_pdf")]
        public bool ShowInAllPdf { get; set; }
        [JsonProperty("show_in_portal")]
        public bool ShowInPortal { get; set; }
        [JsonProperty("show_in_store")]
        public bool ShowInStore { get; set; }
        [JsonProperty("show_on_pdf")]
        public bool ShowOnPdf { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("value_formatted")]
        public string ValueFormatted { get; set; }
    }
}