namespace UnitOfWorkTypesLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public interface IWorkItemFactory<T> where T : DbContext
    {
        IWorkItem Create(Func<T, WorkItemResultEnum> function);
    }
}
