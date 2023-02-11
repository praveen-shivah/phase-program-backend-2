using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace ApiDTO
{

    public class Invoice
    {
        [JsonProperty("includes_package_tracking_info")]
        public bool IncludesPackageTrackingInfo { get; set; }

        [JsonProperty("chatlet_count")]
        public int ChatletCount { get; set; }

        [JsonProperty("can_send_in_mail")]
        public bool CanSendInMail { get; set; }

        [JsonProperty("zcrm_potential_id")]
        public string ZcrmPotentialId { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("taxes")]
        public List<object> Taxes { get; set; }

        [JsonProperty("is_client_review_settings_enabled")]
        public bool IsClientReviewSettingsEnabled { get; set; }

        [JsonProperty("billing_address")]
        public BillingAddress BillingAddress { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("mail_last_viewed_time_formatted")]
        public string MailLastViewedTimeFormatted { get; set; }

        [JsonProperty("payment_expected_date_formatted")]
        public string PaymentExpectedDateFormatted { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("terms")]
        public string Terms { get; set; }

        [JsonProperty("credits_applied")]
        public double CreditsApplied { get; set; }

        [JsonProperty("credits_applied_formatted")]
        public string CreditsAppliedFormatted { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("mail_first_viewed_time")]
        public string MailFirstViewedTime { get; set; }

        [JsonProperty("payment_options")]
        public PaymentOptions PaymentOptions { get; set; }

        [JsonProperty("sub_total_inclusive_of_tax")]
        public double SubTotalInclusiveOfTax { get; set; }

        [JsonProperty("stop_reminder_until_payment_expected_date")]
        public bool StopReminderUntilPaymentExpectedDate { get; set; }

        [JsonProperty("customer_default_billing_address")]
        public CustomerDefaultBillingAddress CustomerDefaultBillingAddress { get; set; }

        [JsonProperty("inprocess_transaction_present")]
        public bool InprocessTransactionPresent { get; set; }

        [JsonProperty("exchange_rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("mail_last_viewed_time")]
        public string MailLastViewedTime { get; set; }

        [JsonProperty("approver_id")]
        public string ApproverId { get; set; }

        [JsonProperty("submitted_date_formatted")]
        public string SubmittedDateFormatted { get; set; }

        [JsonProperty("estimate_id")]
        public string EstimateId { get; set; }

        [JsonProperty("merchant_name")]
        public string MerchantName { get; set; }

        [JsonProperty("sales_channel")]
        public string SalesChannel { get; set; }

        [JsonProperty("customer_custom_fields")]
        public List<object> CustomerCustomFields { get; set; }

        [JsonProperty("shipping_charge_formatted")]
        public string ShippingChargeFormatted { get; set; }

        [JsonProperty("status_formatted")]
        public string StatusFormatted { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("ecomm_operator_name")]
        public string EcommOperatorName { get; set; }

        [JsonProperty("is_autobill_enabled")]
        public bool IsAutobillEnabled { get; set; }

        [JsonProperty("discount_percent")]
        public double DiscountPercent { get; set; }

        [JsonProperty("page_height")]
        public string PageHeight { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("discount_total")]
        public double DiscountTotal { get; set; }

        [JsonProperty("reader_offline_payment_initiated")]
        public bool ReaderOfflinePaymentInitiated { get; set; }

        [JsonProperty("schedule_time_formatted")]
        public string ScheduleTimeFormatted { get; set; }

        [JsonProperty("tax_total")]
        public double TaxTotal { get; set; }

        [JsonProperty("adjustment_formatted")]
        public string AdjustmentFormatted { get; set; }

        [JsonProperty("balance_formatted")]
        public string BalanceFormatted { get; set; }

        [JsonProperty("write_off_amount")]
        public double WriteOffAmount { get; set; }

        [JsonProperty("is_viewed_by_client")]
        public bool IsViewedByClient { get; set; }

        [JsonProperty("salesorder_id")]
        public string SalesorderId { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("page_width")]
        public string PageWidth { get; set; }

        [JsonProperty("cf_reseller_id")]
        public int CfResellerId { get; set; }

        [JsonProperty("sub_statuses")]
        public List<object> SubStatuses { get; set; }

        [JsonProperty("bcy_total")]
        public double BcyTotal { get; set; }

        [JsonProperty("last_reminder_sent_date_formatted")]
        public string LastReminderSentDateFormatted { get; set; }

        [JsonProperty("date_formatted")]
        public string DateFormatted { get; set; }

        [JsonProperty("client_viewed_time_formatted")]
        public string ClientViewedTimeFormatted { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("tax_rounding")]
        public string TaxRounding { get; set; }

        [JsonProperty("salesorders")]
        public List<object> Salesorders { get; set; }

        [JsonProperty("adjustment_description")]
        public string AdjustmentDescription { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("ach_supported")]
        public bool AchSupported { get; set; }

        [JsonProperty("shipping_bills")]
        public List<object> ShippingBills { get; set; }

        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }

        [JsonProperty("transaction_rounding_type")]
        public string TransactionRoundingType { get; set; }

        [JsonProperty("roundoff_value")]
        public double RoundoffValue { get; set; }

        [JsonProperty("contact_persons_details")]
        public List<ContactPersonsDetail> ContactPersonsDetails { get; set; }

        [JsonProperty("template_name")]
        public string TemplateName { get; set; }

        [JsonProperty("deliverychallans")]
        public List<object> Deliverychallans { get; set; }

        [JsonProperty("schedule_time")]
        public string ScheduleTime { get; set; }

        [JsonProperty("salesorder_number")]
        public string SalesorderNumber { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("roundoff_value_formatted")]
        public string RoundoffValueFormatted { get; set; }

        [JsonProperty("total_formatted")]
        public string TotalFormatted { get; set; }

        [JsonProperty("unused_retainer_payments_formatted")]
        public string UnusedRetainerPaymentsFormatted { get; set; }

        [JsonProperty("discount_total_formatted")]
        public string DiscountTotalFormatted { get; set; }

        [JsonProperty("payment_terms_label")]
        public string PaymentTermsLabel { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("submitted_date")]
        public string SubmittedDate { get; set; }

        [JsonProperty("template_type_formatted")]
        public string TemplateTypeFormatted { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("documents")]
        public List<object> Documents { get; set; }

        [JsonProperty("client_viewed_time")]
        public string ClientViewedTime { get; set; }

        [JsonProperty("cf_reseller_id_unformatted")]
        public int CfResellerIdUnformatted { get; set; }

        [JsonProperty("currency_name_formatted")]
        public string CurrencyNameFormatted { get; set; }

        [JsonProperty("ecomm_operator_id")]
        public string EcommOperatorId { get; set; }

        [JsonProperty("last_modified_by_id")]
        public string LastModifiedById { get; set; }

        [JsonProperty("write_off_amount_formatted")]
        public string WriteOffAmountFormatted { get; set; }

        [JsonProperty("payment_discount_formatted")]
        public string PaymentDiscountFormatted { get; set; }

        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("contact_category")]
        public string ContactCategory { get; set; }

        [JsonProperty("template_type")]
        public string TemplateType { get; set; }

        [JsonProperty("recurring_invoice_id")]
        public string RecurringInvoiceId { get; set; }

        [JsonProperty("color_code")]
        public string ColorCode { get; set; }

        [JsonProperty("can_send_invoice_sms")]
        public bool CanSendInvoiceSms { get; set; }

        [JsonProperty("contact_persons")]
        public List<string> ContactPersons { get; set; }

        [JsonProperty("bcy_tax_total")]
        public double BcyTaxTotal { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("created_date_formatted")]
        public string CreatedDateFormatted { get; set; }

        [JsonProperty("last_payment_date_formatted")]
        public string LastPaymentDateFormatted { get; set; }

        [JsonProperty("is_inclusive_tax")]
        public bool IsInclusiveTax { get; set; }

        [JsonProperty("custom_fields")]
        public List<object> CustomFields { get; set; }

        [JsonProperty("last_payment_date")]
        public string LastPaymentDate { get; set; }

        [JsonProperty("price_precision")]
        public double PricePrecision { get; set; }

        [JsonProperty("sub_total_inclusive_of_tax_formatted")]
        public string SubTotalInclusiveOfTaxFormatted { get; set; }

        [JsonProperty("payment_discount")]
        public int PaymentDiscount { get; set; }

        [JsonProperty("current_sub_status_formatted")]
        public string CurrentSubStatusFormatted { get; set; }

        [JsonProperty("approvers_list")]
        public List<object> ApproversList { get; set; }

        [JsonProperty("zcrm_potential_name")]
        public string ZcrmPotentialName { get; set; }

        [JsonProperty("adjustment")]
        public double Adjustment { get; set; }

        [JsonProperty("created_by_id")]
        public string CreatedById { get; set; }

        [JsonProperty("is_backorder")]
        public string IsBackorder { get; set; }

        [JsonProperty("current_sub_status")]
        public string CurrentSubStatus { get; set; }

        [JsonProperty("due_date_formatted")]
        public string DueDateFormatted { get; set; }

        [JsonProperty("offline_created_date_with_time_formatted")]
        public string OfflineCreatedDateWithTimeFormatted { get; set; }

        [JsonProperty("is_discount_before_tax")]
        public bool IsDiscountBeforeTax { get; set; }

        [JsonProperty("attachment_name")]
        public string AttachmentName { get; set; }

        [JsonProperty("ach_payment_initiated")]
        public bool AchPaymentInitiated { get; set; }

        [JsonProperty("last_reminder_sent_date")]
        public string LastReminderSentDate { get; set; }

        [JsonProperty("merchant_id")]
        public string MerchantId { get; set; }

        [JsonProperty("payment_terms")]
        public int PaymentTerms { get; set; }

        [JsonProperty("mail_first_viewed_time_formatted")]
        public string MailFirstViewedTimeFormatted { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("tax_total_formatted")]
        public string TaxTotalFormatted { get; set; }

        [JsonProperty("current_sub_status_id")]
        public string CurrentSubStatusId { get; set; }

        [JsonProperty("custom_field_hash")]
        public CustomFieldHash CustomFieldHash { get; set; }

        [JsonProperty("sub_total_formatted")]
        public string SubTotalFormatted { get; set; }

        [JsonProperty("tax_amount_withheld")]
        public double TaxAmountWithheld { get; set; }

        [JsonProperty("tax_amount_withheld_formatted")]
        public string TaxAmountWithheldFormatted { get; set; }

        [JsonProperty("is_viewed_in_mail")]
        public bool IsViewedInMail { get; set; }

        [JsonProperty("bcy_shipping_charge")]
        public double BcyShippingCharge { get; set; }

        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonProperty("next_reminder_date_formatted")]
        public string NextReminderDateFormatted { get; set; }

        [JsonProperty("bcy_discount_total")]
        public int BcyDiscountTotal { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("discount_applied_on_amount")]
        public double DiscountAppliedOnAmount { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("submitter_id")]
        public string SubmitterId { get; set; }

        [JsonProperty("submitted_by")]
        public string SubmittedBy { get; set; }

        [JsonProperty("subject_content")]
        public string SubjectContent { get; set; }

        [JsonProperty("payment_made_formatted")]
        public string PaymentMadeFormatted { get; set; }

        [JsonProperty("bcy_sub_total")]
        public double BcySubTotal { get; set; }

        [JsonProperty("payment_expected_date")]
        public string PaymentExpectedDate { get; set; }

        [JsonProperty("is_emailed")]
        public bool IsEmailed { get; set; }

        [JsonProperty("reminders_sent")]
        public int RemindersSent { get; set; }

        [JsonProperty("unused_retainer_payments")]
        public int UnusedRetainerPayments { get; set; }

        [JsonProperty("offline_created_date_with_time")]
        public string OfflineCreatedDateWithTime { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("salesperson_id")]
        public string SalespersonId { get; set; }

        [JsonProperty("payment_made")]
        public double PaymentMade { get; set; }

        [JsonProperty("shipping_charge")]
        public double ShippingCharge { get; set; }

        [JsonProperty("bcy_adjustment")]
        public double BcyAdjustment { get; set; }

        [JsonProperty("sub_total")]
        public double SubTotal { get; set; }

        [JsonProperty("allow_partial_payments")]
        public bool AllowPartialPayments { get; set; }

        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("customer_custom_field_hash")]
        public CustomerCustomFieldHash CustomerCustomFieldHash { get; set; }

        [JsonProperty("currency_id")]
        public string CurrencyId { get; set; }

        [JsonProperty("invoice_url")]
        public string InvoiceUrl { get; set; }

        [JsonProperty("payment_reminder_enabled")]
        public bool PaymentReminderEnabled { get; set; }
    }
}