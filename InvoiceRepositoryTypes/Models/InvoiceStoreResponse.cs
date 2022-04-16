namespace InvoiceRepositoryTypes
{
    public enum InvoiceStoreResponseType
    {
        notSet,
        success,
        databaseError
    }

    public class InvoiceStoreResponse
    {
        public bool IsSuccessful { get; set; }
        public InvoiceStoreResponseType InvoiceStoreResponseType { get; set; }
    }
}
