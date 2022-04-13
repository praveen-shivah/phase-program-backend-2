namespace UnitOfWorkTypesLibrary
{
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkContextContainerGeneric<T> where T : DbContext
    {
        T CurrentDbContext { get; }
        int NumberOfTransactions { get; set; }
        void Refresh();
    }
}