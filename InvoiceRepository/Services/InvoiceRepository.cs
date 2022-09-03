namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using LoggingLibrary;

    using UnitOfWorkTypesLibrary;

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IInvoiceStore invoiceStore;

        private readonly ILogger logger;

        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public InvoiceRepository(
            IUnitOfWorkFactory<DPContext> unitOfWorkFactory,
            IInvoiceStore invoiceStore,
            ILogger logger)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.invoiceStore = invoiceStore;
            this.logger = logger;
        }

        async Task<InvoiceStoreResponse> IInvoiceRepository.Store(InvoiceStoreRequest request)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var invoiceStoreResponse = await this.invoiceStore.Store(context, request);
                        if (invoiceStoreResponse.IsSuccessful)
                        {
                            return WorkItemResultEnum.doneContinue;
                        }

                        this.logger.Error(
                            LogClass.General,
                            "InvoiceRepository",
                            "Store",
                            $"Error storing invoice information - reason: {invoiceStoreResponse.InvoiceStoreResponseType}",
                            new Exception($"Error storing invoice information - reason: {invoiceStoreResponse.InvoiceStoreResponseType}"));

                        return WorkItemResultEnum.rollbackExit;
                    });

            var result = await uow.ExecuteAsync();
            return new InvoiceStoreResponse
                       {
                           IsSuccessful = result == WorkItemResultEnum.commitSuccessfullyCompleted,
                           InvoiceStoreResponseType = result == WorkItemResultEnum.commitSuccessfullyCompleted
                                                          ? InvoiceStoreResponseType.success
                                                          : InvoiceStoreResponseType.databaseError
                       };
        }
    }
}