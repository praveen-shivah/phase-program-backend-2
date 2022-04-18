namespace InvoiceRepository
{
    using System.ComponentModel.Design.Serialization;

    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using MobileRequestApiDTO;

    using Newtonsoft.Json;

    internal class InvoiceStoreDeserialize : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        public InvoiceStoreDeserialize(IInvoiceStore invoiceStore)
        {
            this.invoiceStore = invoiceStore;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var root = JsonConvert.DeserializeObject<Root>(request.JsonString);
            if (root == null)
            {
                response.IsSuccessful = false;
                response.InvoiceStoreResponseType = InvoiceStoreResponseType.jsonDeserializationError;
                return response;
            }

            response.Invoice = root.Invoice;

            return response;
        }
    }
}
