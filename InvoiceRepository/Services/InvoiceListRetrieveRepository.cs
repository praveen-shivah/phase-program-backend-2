namespace InvoiceRepository;

using DatabaseContext;

using UnitOfWorkTypesLibrary;

public class InvoiceListRetrieveRepository : IInvoiceListRetrieveRepository
{
    private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

    private readonly IInvoiceListRetrieve invoiceListRetrieve;

    public InvoiceListRetrieveRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IInvoiceListRetrieve invoiceListRetrieve)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.invoiceListRetrieve = invoiceListRetrieve;
    }

    async Task<InvoiceListRetrieveResponse> IInvoiceListRetrieveRepository.InvoiceListRetrieveAsync(InvoiceListRetrieveRequest request)
    {
        var result = new InvoiceListRetrieveResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.invoiceListRetrieve.InvoiceListRetrieveAsync(context, request);
                    result.IsSuccessful = response.IsSuccessful;

                    if (response.IsSuccessful)
                    {
                        result.InvoiceList = response.InvoiceList;
                        return WorkItemResultEnum.commitSuccessfullyCompleted;
                    }
                    else
                    {
                        return WorkItemResultEnum.rollbackExit;
                    }
                });
        var uowResult = await uow.ExecuteAsync();
        if (uowResult != WorkItemResultEnum.commitSuccessfullyCompleted)
        {
            result.IsSuccessful = false;
        }

        return result;
    }
}