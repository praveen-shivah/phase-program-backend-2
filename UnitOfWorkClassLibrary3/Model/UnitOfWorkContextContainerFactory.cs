namespace UnitOfWorkClassLibrary
{
    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class UnitOfWorkContextContainerFactory<T> : IUnitOfWorkContextContainerFactory<T> where T : DbContext
    {
        private readonly IEntityContextFrameWorkFactory<T> entityContextFrameWorkFactory;

        public UnitOfWorkContextContainerFactory(IEntityContextFrameWorkFactory<T> entityContextFrameWorkFactory)
        {
            this.entityContextFrameWorkFactory = entityContextFrameWorkFactory;
        }

        IUnitOfWorkContextContainer<T> IUnitOfWorkContextContainerFactory<T>.CreateContainer()
        {
            return new UnitOfWorkContextContainer<T>(this.entityContextFrameWorkFactory);
        }
    }
}