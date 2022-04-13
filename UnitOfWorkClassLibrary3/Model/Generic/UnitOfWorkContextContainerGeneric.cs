namespace UnitOfWorkClassLibrary
{
    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class UnitOfWorkContextContainerGeneric<T> : IUnitOfWorkContextContainerGeneric<T> where T : DbContext
    {
        private readonly IEntityContextFrameWorkFactoryGeneric<T> entityContextFrameWorkFactory;
        private T context;
        private int numberOfTransactions;

        public UnitOfWorkContextContainerGeneric(IEntityContextFrameWorkFactoryGeneric<T> entityContextFrameWorkFactory)
        {
            this.entityContextFrameWorkFactory = entityContextFrameWorkFactory;
        }        
        
        T IUnitOfWorkContextContainerGeneric<T>.CurrentDbContext
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

        int IUnitOfWorkContextContainerGeneric<T>.NumberOfTransactions
        {
            get => this.numberOfTransactions;
            set => this.numberOfTransactions = value;
        }

        void IUnitOfWorkContextContainerGeneric<T>.Refresh()
        {
            this.context?.CloseConnection();
            this.context?.Dispose();
            this.context = default(T);
            this.numberOfTransactions = 0;
        }
    }
}