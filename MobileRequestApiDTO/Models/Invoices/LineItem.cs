using Newtonsoft.Json;
namespace ApiDTO
{
    public class LineItem
    {
        [JsonProperty("discount_amount_formatted")]
        public string DiscountAmountFormatted { get; set; }

        [JsonProperty("bcy_rate")]
        public double BcyRate { get; set; }

        [JsonProperty("item_total_formatted")]
        public string ItemTotalFormatted { get; set; }

        [JsonProperty("salesorder_item_id")]
        public string SalesorderItemId { get; set; }

        [JsonProperty("line_item_id")]
        public string LineItemId { get; set; }

        [JsonProperty("rate_formatted")]
        public string RateFormatted { get; set; }

        [JsonProperty("header_id")]
        public string HeaderId { get; set; }

        [JsonProperty("documents")]
        public List<object> Documents { get; set; }

        [JsonProperty("discount_amount")]
        public int DiscountAmount { get; set; }

        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("item_type_formatted")]
        public string ItemTypeFormatted { get; set; }

        [JsonProperty("purchase_rate")]
        public string PurchaseRate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("item_order")]
        public int ItemOrder { get; set; }

        [JsonProperty("zoho_recruit_jobopening_id")]
        public string ZohoRecruitJobopeningId { get; set; }

        [JsonProperty("discounts")]
        public List<object> Discounts { get; set; }

        [JsonProperty("bill_item_id")]
        public string BillItemId { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("pricebook_id")]
        public string PricebookId { get; set; }

        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("bcy_rate_formatted")]
        public string BcyRateFormatted { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("image_document_id")]
        public string ImageDocumentId { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("expense_receipt_name")]
        public string ExpenseReceiptName { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("item_total")]
        public double ItemTotal { get; set; }

        [JsonProperty("header_name")]
        public string HeaderName { get; set; }

        [JsonProperty("item_custom_fields")]
        public List<ItemCustomField> ItemCustomFields { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("purchase_rate_formatted")]
        public string PurchaseRateFormatted { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("cost_amount")]
        public int CostAmount { get; set; }

        [JsonProperty("tax_type")]
        public string TaxType { get; set; }

        [JsonProperty("time_entry_ids")]
        public List<object> TimeEntryIds { get; set; }

        [JsonProperty("cost_amount_formatted")]
        public string CostAmountFormatted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tax_percentage")]
        public int TaxPercentage { get; set; }

        [JsonProperty("expense_id")]
        public string ExpenseId { get; set; }
    }
}