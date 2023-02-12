namespace InvoiceRepository;

using DatabaseContext;

using UnitOfWorkTypesLibrary;

public class InvoiceListResellerRetrieveRepository : IInvoiceListResellerRetrieveRepository
{
    private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

    private readonly IInvoiceListResellerRetrieve invoiceListResellerRetrieve;

    public InvoiceListResellerRetrieveRepository(IUnitOfWorkFactory<DataContext> unitOfWorkFactory, IInvoiceListResellerRetrieve invoiceListResellerRetrieve)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.invoiceListResellerRetrieve = invoiceListResellerRetrieve;
    }

    async Task<InvoiceListResellerRetrieveResponse> IInvoiceListResellerRetrieveRepository.InvoiceListResellerRetrieveAsync(InvoiceListResellerRetrieveRequest request)
    {
        var result = new InvoiceListResellerRetrieveResponse();
        var uow = this.unitOfWorkFactory.Create(
            async context =>
                {
                    var response = await this.invoiceListResellerRetrieve.InvoiceListResellerRetrieveAsync(context, request);
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