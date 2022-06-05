namespace InvoiceRepository
{
    using ApplicationLifeCycle;

    using InvoiceRepositoryTypes;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IInvoiceRepository, InvoiceRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<IInvoiceStore, InvoiceStoreStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreDeserialize>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreCreateInvoice>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreAddJsonRevision>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreUpdateInvoice>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IInvoiceStore, InvoiceStoreSendTransferRequest>(Lifestyle.Transient);

            return true;
        }
    }
}
