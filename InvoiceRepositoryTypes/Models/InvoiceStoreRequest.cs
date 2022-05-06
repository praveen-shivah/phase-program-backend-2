namespace InvoiceRepositoryTypes
{
    public class InvoiceStoreRequest
    {
        public InvoiceStoreRequest(int organizationId, string jsonString)
        {
            this.OrganizationId = organizationId;
            this.JsonString = jsonString;
        }

        public int OrganizationId { get; }

        public string JsonString { get; }
    }
}