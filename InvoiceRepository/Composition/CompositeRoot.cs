namespace UnitOfWorkClassLibrary
{
    using ApplicationLifeCycle;

    using InvoiceRepository;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IInvoiceStore, InvoiceStoreStart>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreDeserialize>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreCreateInvoice>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreAddJsonRevision>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreUpdateInvoice>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreSendTransferRequest>(Lifestyle.Singleton);

            return true;
        }
    }
}
