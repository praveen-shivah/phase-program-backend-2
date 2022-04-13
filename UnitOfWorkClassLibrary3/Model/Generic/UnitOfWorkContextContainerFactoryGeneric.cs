namespace UnitOfWorkClassLibrary
{
    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class UnitOfWorkContextContainerFactoryGeneric<T> : IUnitOfWorkContextContainerFactoryGeneric<T> where T : DbContext
    {
        private readonly IEntityContextFrameWorkFactoryGeneric<T> entityContextFrameWorkFactory;

        public UnitOfWorkContextContainerFactoryGeneric(IEntityContextFrameWorkFactoryGeneric<T> entityContextFrameWorkFactory)
        {
            this.entityContextFrameWorkFactory = entityContextFrameWorkFactory;
        }

        IUnitOfWorkContextContainerGeneric<T> IUnitOfWorkContextContainerFactoryGeneric<T>.CreateContainer()
        {
            return new UnitOfWorkContextContainerGeneric<T>(this.entityContextFrameWorkFactory);
        }
    }
}