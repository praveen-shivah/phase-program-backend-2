namespace VendorRepository
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    using VendorRepositoryTypes;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IVendorRepository, VendorRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateVendor, UpdateVendorStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateVendor, UpdateVendorRetrieveVendor>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateVendor, UpdateVendorUpdate>(Lifestyle.Transient);

            return true;
        }
    }
}
