namespace UnitOfWorkClassLibrary
{
    using System;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class UnitOfWorkFactoryGeneric<T> : IUnitOfWorkFactoryGeneric<T> where T : DbContext
    {
        private readonly ILogger logger;
        private readonly IUnitOfWorkContextContainerFactoryGeneric<T> unitOfWorkContextContainerFactory;
        private readonly IWorkItemFactoryGeneric<T> workItemFactory;

        public UnitOfWorkFactoryGeneric(ILogger logger, IUnitOfWorkContextContainerFactoryGeneric<T> unitOfWorkContextContainerFactory, IWorkItemFactoryGeneric<T> workItemFactory)
        {
            this.logger = logger;
            this.unitOfWorkContextContainerFactory = unitOfWorkContextContainerFactory;
            this.workItemFactory = workItemFactory;
        }

        IUnitOfWork IUnitOfWorkFactoryGeneric<T>.Create(Func<T, WorkItemResultEnum> function)
        {
            IUnitOfWork result = new UnitOfWork<T>(this.logger, this.unitOfWorkContextContainerFactory.CreateContainer());
            result.AddWorkItem(this.workItemFactory.Create(function));
            return result;
        }
    }
}
