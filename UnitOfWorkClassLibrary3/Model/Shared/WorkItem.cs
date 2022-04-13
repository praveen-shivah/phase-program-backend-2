namespace UnitOfWorkClassLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class WorkItem : IWorkItem
    {
        private readonly Func<DbContext, WorkItemResultEnum> function;

        public WorkItem(Func<DbContext, WorkItemResultEnum> function)
        {
            this.function = function;
        }

        WorkItemResultEnum IWorkItem.Execute(DbContext context)
        {
            return this.function((DbContext)context);
        }
    }
}