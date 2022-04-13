namespace UnitOfWorkTypesLibrary
{
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkContextContainer<T> where T : DbContext
    {
        T CurrentDbContext { get; }
        int NumberOfTransactions { get; set; }
        void Refresh();
    }
}