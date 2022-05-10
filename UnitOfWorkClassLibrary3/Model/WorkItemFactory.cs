namespace UnitOfWorkClassLibrary
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class WorkItemFactory<T> : IWorkItemFactory<T> where T : DbContext
    {
        IWorkItem IWorkItemFactory<T>.Create(Func<T, Task<WorkItemResultEnum>> function)
        {
            return new WorkItem<T>(function);
        }
    }
}
