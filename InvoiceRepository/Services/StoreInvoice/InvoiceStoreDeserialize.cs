﻿namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using LoggingLibrary;

    using MobileRequestApiDTO;

    using Newtonsoft.Json;

    internal class InvoiceStoreDeserialize : IInvoiceStore
    {
        private readonly IInvoiceStore invoiceStore;

        private readonly ILogger logger;

        public InvoiceStoreDeserialize(IInvoiceStore invoiceStore, ILogger logger)
        {
            this.invoiceStore = invoiceStore;
            this.logger = logger;
        }

        async Task<InvoiceStoreResponse> IInvoiceStore.Store(DPContext dpContext, InvoiceStoreRequest request)
        {
            var response = await this.invoiceStore.Store(dpContext, request);
            if (!response.IsSuccessful)
            {
                return response;
            }

            try
            {
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
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.InvoiceStoreResponseType = InvoiceStoreResponseType.jsonDeserializationError;
                this.logger.Error(LogClass.General, "InvoiceStoreDeserializer", "Store", $"Error: {e.Message} {e.StackTrace}", e);
            }

            return response;
        }
    }
}
