namespace UnitOfWorkClassLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class WorkItemGeneric<T> : IWorkItem where T : DbContext
    {
        private readonly Func<T, WorkItemResultEnum> function;

        public WorkItemGeneric(Func<T, WorkItemResultEnum> function)
        {
            this.function = function;
        }

        WorkItemResultEnum IWorkItem.Execute(DbContext context)
        {
            return this.function((T)context);
        }
    }
}