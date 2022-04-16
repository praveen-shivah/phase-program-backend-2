namespace InvoiceRepositoryTypes
{
    public class InvoiceStoreRequest
    {
        public InvoiceStoreRequest(string jsonString)
        {
            this.JsonString = jsonString;
        }

        public string JsonString { get; }
    }
}