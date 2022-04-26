namespace UnitOfWorkClassLibrary
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class WorkItem<T> : IWorkItem where T : DbContext
    {
        private readonly Func<T, Task<WorkItemResultEnum>> function;

        public WorkItem(Func<T, Task<WorkItemResultEnum>> function)
        {
            this.function = function;
        }

        async Task<WorkItemResultEnum> IWorkItem.ExecuteAsync(DbContext context)
        {
            return await this.function((T)context);
        }
    }
}