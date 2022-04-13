namespace UnitOfWorkClassLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class WorkItemFactoryGeneric<T> : IWorkItemFactoryGeneric<T> where T : DbContext
    {
        IWorkItem IWorkItemFactoryGeneric<T>.Create(Func<T, WorkItemResultEnum> function)
        {
            return new WorkItemGeneric<T>(function);
        }
    }
}
