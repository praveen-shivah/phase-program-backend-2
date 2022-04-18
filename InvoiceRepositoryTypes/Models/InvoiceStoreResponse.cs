﻿namespace InvoiceRepositoryTypes
{
    using MobileRequestApiDTO;

    public enum InvoiceStoreResponseType
    {
        notSet,
        success,
        databaseError,
        jsonDeserializationError
    }

    public class InvoiceStoreResponse
    {
        public bool IsSuccessful { get; set; }
        public InvoiceStoreResponseType InvoiceStoreResponseType { get; set; }

        public Invoice Invoice { get; set; }

        public DataModelsLibrary.Invoice InvoiceRecord { get; set; }
    }
}
