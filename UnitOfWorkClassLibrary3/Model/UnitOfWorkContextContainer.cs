namespace UnitOfWorkClassLibrary
{
    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class UnitOfWorkContextContainer<T> : IUnitOfWorkContextContainer<T> where T : DbContext
    {
        private readonly IEntityContextFrameWorkFactory<T> entityContextFrameWorkFactory;
        private T context;
        private int numberOfTransactions;

        public UnitOfWorkContextContainer(IEntityContextFrameWorkFactory<T> entityContextFrameWorkFactory)
        {
            this.entityContextFrameWorkFactory = entityContextFrameWorkFactory;
        }

        T IUnitOfWorkContextContainer<T>.CurrentDbContext
        {
            get
            {
                if (this.context != null)
                {
                    return this.context;
                }

                this.numberOfTransactions = 0;
                this.context = this.entityContextFrameWorkFactory.CreateContext();
                return this.context;
            }
        }

        int IUnitOfWorkContextContainer<T>.NumberOfTransactions
        {
            get => this.numberOfTransactions;
            set => this.numberOfTransactions = value;
        }

        void IUnitOfWorkContextContainer<T>.Refresh()
        {
            this.context?.Dispose();
            this.context = default(T);
            this.numberOfTransactions = 0;
        }
    }
}