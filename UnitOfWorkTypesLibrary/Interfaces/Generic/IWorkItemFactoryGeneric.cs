namespace UnitOfWorkTypesLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public interface IWorkItemFactoryGeneric<T> where T : DbContext
    {
        IWorkItem Create(Func<T, WorkItemResultEnum> function);
    }
}
