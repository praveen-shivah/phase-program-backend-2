namespace UnitOfWorkClassLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class WorkItemFactoryGeneric : IWorkItemFactory
    {
        IWorkItem IWorkItemFactory.Create(Func<DbContext, WorkItemResultEnum> function)
        {
            return new WorkItem(function);
        }
    }
}
