namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class LineItem : BaseEntity
    {
        public string AccountId { get; set; }

        public string AccountName { get; set; }

        public int BcyRate { get; set; }

        public string BcyRateFormatted { get; set; }

        public string BillId { get; set; }

        public string BillItemId { get; set; }

        public int CostAmount { get; set; }

        public string CostAmountFormatted { get; set; }

        public string Description { get; set; }

        public int Discount { get; set; }

        public int DiscountAmount { get; set; }

        public string DiscountAmountFormatted { get; set; }

        public List<object> Discounts { get; set; }

        public List<object> Documents { get; set; }

        public string ExpenseId { get; set; }

        public string ExpenseReceiptName { get; set; }

        public string HeaderId { get; set; }

        public string HeaderName { get; set; }

        public string ImageDocumentId { get; set; }

        public List<ItemCustomField> ItemCustomFields { get; set; }

        public string ItemId { get; set; }

        public int ItemOrder { get; set; }

        public int ItemTotal { get; set; }

        public string ItemTotalFormatted { get; set; }

        public string ItemType { get; set; }

        public string ItemTypeFormatted { get; set; }

        public string LineItemId { get; set; }

        public string Name { get; set; }

        public string PricebookId { get; set; }

        public string ProjectId { get; set; }

        public int PurchaseRate { get; set; }

        public string PurchaseRateFormatted { get; set; }

        public int Quantity { get; set; }

        public int Rate { get; set; }

        public string RateFormatted { get; set; }

        public string SalesorderItemId { get; set; }

        public List<object> Tags { get; set; }

        public string TaxId { get; set; }

        public string TaxName { get; set; }

        public int TaxPercentage { get; set; }

        public string TaxType { get; set; }

        public List<object> TimeEntryIds { get; set; }

        public string Unit { get; set; }

        public string ZohoRecruitJobopeningId { get; set; }
    }
}