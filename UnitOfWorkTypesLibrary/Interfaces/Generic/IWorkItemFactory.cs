namespace UnitOfWorkTypesLibrary
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public interface IWorkItemFactory<T> where T : DbContext
    {
        IWorkItem Create(Func<T, Task<WorkItemResultEnum>> function);
    }
}
