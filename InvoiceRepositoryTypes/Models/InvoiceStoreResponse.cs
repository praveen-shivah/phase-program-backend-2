﻿namespace InvoiceRepositoryTypes
{
    using DataModelsLibrary;

    using Invoice = ApiDTO.Invoice;

    public enum InvoiceStoreResponseType
    {
        notSet,
        success,
        databaseError,
        invalidOrganizationId,
        invalidResellerId,
        jsonDeserializationError
    }

    public class InvoiceStoreResponse
    {
        public bool IsSuccessful { get; set; }
        public InvoiceStoreResponseType InvoiceStoreResponseType { get; set; }

        public Invoice Invoice { get; set; }

        public DataModelsLibrary.Invoice InvoiceRecord { get; set; }

        public Organization? Organization { get; set; }
    }
}
