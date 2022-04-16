namespace DataModelsLibrary
{
    using DataSharedLibrary;

    public class Invoice : BaseEntity
    {
        public bool AchPaymentInitiated { get; set; }

        public bool AchSupported { get; set; }

        public int Adjustment { get; set; }

        public string AdjustmentDescription { get; set; }

        public string AdjustmentFormatted { get; set; }

        public bool AllowPartialPayments { get; set; }

        public string ApproverId { get; set; }

        public List<object> ApproversList { get; set; }

        public string AttachmentName { get; set; }

        public int Balance { get; set; }

        public string BalanceFormatted { get; set; }

        public int BcyAdjustment { get; set; }

        public int BcyDiscountTotal { get; set; }

        public int BcyShippingCharge { get; set; }

        public int BcySubTotal { get; set; }

        public int BcyTaxTotal { get; set; }

        public int BcyTotal { get; set; }

        public BillingAddress BillingAddress { get; set; }

        public bool CanSendInMail { get; set; }

        public bool CanSendInvoiceSms { get; set; }

        public string CfCustomerType { get; set; }

        public string CfCustomerTypeUnformatted { get; set; }

        public string CfSiteNumber { get; set; }

        public string CfSiteNumberUnformatted { get; set; }

        public int ChatletCount { get; set; }

        public string ClientViewedTime { get; set; }

        public string ClientViewedTimeFormatted { get; set; }

        public string ColorCode { get; set; }

        public string ContactCategory { get; set; }

        public List<string> ContactPersons { get; set; }

        public List<ContactPersonsDetail> ContactPersonsDetails { get; set; }

        public string CreatedById { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedDateFormatted { get; set; }

        public DateTime CreatedTime { get; set; }

        public int CreditsApplied { get; set; }

        public string CreditsAppliedFormatted { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencyId { get; set; }

        public string CurrencySymbol { get; set; }

        public string CurrentSubStatus { get; set; }

        public string CurrentSubStatusFormatted { get; set; }

        public string CurrentSubStatusId { get; set; }

        public CustomerCustomFieldHash CustomerCustomFieldHash { get; set; }

        public List<CustomerCustomField> CustomerCustomFields { get; set; }

        public CustomerDefaultBillingAddress CustomerDefaultBillingAddress { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public CustomFieldHash CustomFieldHash { get; set; }

        public List<object> CustomFields { get; set; }

        public string Date { get; set; }

        public string DateFormatted { get; set; }

        public List<object> Deliverychallans { get; set; }

        public int Discount { get; set; }

        public int DiscountAppliedOnAmount { get; set; }

        public int DiscountPercent { get; set; }

        public int DiscountTotal { get; set; }

        public string DiscountTotalFormatted { get; set; }

        public string DiscountType { get; set; }

        public List<object> Documents { get; set; }

        public string DueDate { get; set; }

        public string DueDateFormatted { get; set; }

        public string EcommOperatorId { get; set; }

        public string EcommOperatorName { get; set; }

        public string Email { get; set; }

        public string EstimateId { get; set; }

        public int ExchangeRate { get; set; }

        public bool IncludesPackageTrackingInfo { get; set; }

        public bool InprocessTransactionPresent { get; set; }

        public string InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceUrl { get; set; }

        public bool IsAutobillEnabled { get; set; }

        public string IsBackorder { get; set; }

        public bool IsClientReviewSettingsEnabled { get; set; }

        public bool IsDiscountBeforeTax { get; set; }

        public bool IsEmailed { get; set; }

        public bool IsInclusiveTax { get; set; }

        public bool IsViewedByClient { get; set; }

        public bool IsViewedInMail { get; set; }

        public string LastModifiedById { get; set; }

        public DateTime LastModifiedTime { get; set; }

        public string LastPaymentDate { get; set; }

        public string LastPaymentDateFormatted { get; set; }

        public string LastReminderSentDate { get; set; }

        public string LastReminderSentDateFormatted { get; set; }

        public List<LineItem> LineItems { get; set; }

        public string MailFirstViewedTime { get; set; }

        public string MailFirstViewedTimeFormatted { get; set; }

        public string MailLastViewedTime { get; set; }

        public string MailLastViewedTimeFormatted { get; set; }

        public string MerchantId { get; set; }

        public string MerchantName { get; set; }

        public string NextReminderDateFormatted { get; set; }

        public string Notes { get; set; }

        public string OfflineCreatedDateWithTime { get; set; }

        public string OfflineCreatedDateWithTimeFormatted { get; set; }

        public string Orientation { get; set; }

        public string PageHeight { get; set; }

        public string PageWidth { get; set; }

        public int PaymentDiscount { get; set; }

        public string PaymentDiscountFormatted { get; set; }

        public string PaymentExpectedDate { get; set; }

        public string PaymentExpectedDateFormatted { get; set; }

        public int PaymentMade { get; set; }

        public string PaymentMadeFormatted { get; set; }

        public PaymentOptions PaymentOptions { get; set; }

        public bool PaymentReminderEnabled { get; set; }

        public int PaymentTerms { get; set; }

        public string PaymentTermsLabel { get; set; }

        public int PricePrecision { get; set; }

        public bool ReaderOfflinePaymentInitiated { get; set; }

        public string RecurringInvoiceId { get; set; }

        public string ReferenceNumber { get; set; }

        public int RemindersSent { get; set; }

        public int RoundoffValue { get; set; }

        public string RoundoffValueFormatted { get; set; }

        public string SalesChannel { get; set; }

        public string SalesorderId { get; set; }

        public string SalesorderNumber { get; set; }

        public List<object> Salesorders { get; set; }

        public string SalespersonId { get; set; }

        public string SalespersonName { get; set; }

        public string ScheduleTime { get; set; }

        public string ScheduleTimeFormatted { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public List<object> ShippingBills { get; set; }

        public int ShippingCharge { get; set; }

        public string ShippingChargeFormatted { get; set; }

        public string Status { get; set; }

        public string StatusFormatted { get; set; }

        public bool StopReminderUntilPaymentExpectedDate { get; set; }

        public string SubjectContent { get; set; }

        public string SubmittedBy { get; set; }

        public string SubmittedDate { get; set; }

        public string SubmittedDateFormatted { get; set; }

        public string SubmitterId { get; set; }

        public List<object> SubStatuses { get; set; }

        public int SubTotal { get; set; }

        public string SubTotalFormatted { get; set; }

        public int SubTotalInclusiveOfTax { get; set; }

        public string SubTotalInclusiveOfTaxFormatted { get; set; }

        public int TaxAmountWithheld { get; set; }

        public string TaxAmountWithheldFormatted { get; set; }

        public List<object> Taxes { get; set; }

        public string TaxRounding { get; set; }

        public int TaxTotal { get; set; }

        public string TaxTotalFormatted { get; set; }

        public string TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string TemplateType { get; set; }

        public string TemplateTypeFormatted { get; set; }

        public string Terms { get; set; }

        public int Total { get; set; }

        public string TotalFormatted { get; set; }

        public string TransactionRoundingType { get; set; }

        public int UnusedRetainerPayments { get; set; }

        public string UnusedRetainerPaymentsFormatted { get; set; }

        public int WriteOffAmount { get; set; }

        public string WriteOffAmountFormatted { get; set; }

        public string ZcrmPotentialId { get; set; }

        public string ZcrmPotentialName { get; set; }
    }
}