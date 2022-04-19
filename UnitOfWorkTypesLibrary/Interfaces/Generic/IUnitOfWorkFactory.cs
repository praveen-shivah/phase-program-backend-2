namespace UnitOfWorkTypesLibrary
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkFactory<T> where T : DbContext
    {
        IUnitOfWork Create(Func<T, Task<WorkItemResultEnum>> function);
    }
}
