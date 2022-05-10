namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using UnitOfWorkTypesLibrary;

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        private readonly IInvoiceStore invoiceStore;

        public InvoiceRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory, IInvoiceStore invoiceStore)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.invoiceStore = invoiceStore;
        }

        async Task<InvoiceStoreResponse> IInvoiceRepository.Store(InvoiceStoreRequest request)
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        await this.invoiceStore.Store(context, request);
                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            return new InvoiceStoreResponse()
            {
                IsSuccessful = result == WorkItemResultEnum.commitSuccessfullyCompleted,
                InvoiceStoreResponseType = result == WorkItemResultEnum.commitSuccessfullyCompleted ? InvoiceStoreResponseType.success : InvoiceStoreResponseType.databaseError
            };
        }
    }
}
