namespace UnitOfWorkTypesLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public interface IWorkItemFactory
    {
        IWorkItem Create(Func<DbContext, WorkItemResultEnum> function);
    }
}
