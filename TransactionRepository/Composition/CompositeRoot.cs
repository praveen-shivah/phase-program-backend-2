namespace TransactionRepository
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    using TransactionRepositoryTypes;

    public class CompositeRoot:CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<ITransactionRepository, TransactionRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<ICreateTransaction, CreateTransactionStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<ICreateTransaction, CreateTransactionCreate>(Lifestyle.Transient);

            return true;
        }
    }
}
