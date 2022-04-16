namespace InvoiceRepository
{
    using DataPostgresqlLibrary;

    using InvoiceRepositoryTypes;

    using UnitOfWorkTypesLibrary;

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public InvoiceRepository(IUnitOfWorkFactory<DPContext> unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        async Task<InvoiceStoreResponse> IInvoiceRepository.Store(InvoiceStoreRequest request)
        {
            var uow = this.unitOfWorkFactory.Create(
                context =>
                    {
                        // Store json string in a revision table
                        // create invoice if does not exist
                        // create line items
                        // if requires automation, then add to service bus to get 
                        // response will update invoice in zoho
                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.Execute();
            return new InvoiceStoreResponse()
            {
                IsSuccessful = result == WorkItemResultEnum.doneContinue,
                InvoiceStoreResponseType = result == WorkItemResultEnum.doneContinue ? InvoiceStoreResponseType.success : InvoiceStoreResponseType.databaseError
            };
        }
    }
}
