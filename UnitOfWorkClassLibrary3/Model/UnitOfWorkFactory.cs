namespace UnitOfWorkClassLibrary
{
    using System;
    using System.Threading.Tasks;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T> where T : DbContext
    {
        private readonly ILogger logger;
        private readonly IUnitOfWorkContextContainerFactory<T> unitOfWorkContextContainerFactory;
        private readonly IWorkItemFactory<T> workItemFactory;

        public UnitOfWorkFactory(ILogger logger, IUnitOfWorkContextContainerFactory<T> unitOfWorkContextContainerFactory, IWorkItemFactory<T> workItemFactory)
        {
            this.logger = logger;
            this.unitOfWorkContextContainerFactory = unitOfWorkContextContainerFactory;
            this.workItemFactory = workItemFactory;
        }

        IUnitOfWork IUnitOfWorkFactory<T>.Create(Func<T, WorkItemResultEnum> function)
        {
            IUnitOfWork result = new UnitOfWork<T>(this.logger, this.unitOfWorkContextContainerFactory.CreateContainer());
            result.AddWorkItem(this.workItemFactory.Create(function));
            return result;
        }
    }
}
