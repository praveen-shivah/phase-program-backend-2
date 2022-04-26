namespace InvoiceRepositoryTypes
{
    public class InvoiceStoreRequest
    {
        public InvoiceStoreRequest(string organizationId, string jsonString)
        {
            this.OrganizationId = organizationId;
            this.JsonString = jsonString;
        }

        public string OrganizationId { get; }

        public string JsonString { get; }
    }
}