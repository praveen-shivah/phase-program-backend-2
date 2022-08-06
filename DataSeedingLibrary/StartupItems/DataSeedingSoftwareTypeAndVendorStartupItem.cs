namespace DataSeedingLibrary
{
    using ApiDTO;

    using ApplicationLifeCycle;

    using DataModelsLibrary;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    using UnitOfWorkTypesLibrary;

    public class DataSeedingSoftwareTypeAndVendorStartupItem : IRequestLifeCycleStartupItem
    {
        private readonly IUnitOfWorkFactory<DPContext> unitOfWorkFactory;

        public DataSeedingSoftwareTypeAndVendorStartupItem(IUnitOfWorkFactory<DPContext> unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        RequestLifeCycleStartupItemPriority IRequestLifeCycleStartupItem.RequestLifeCycleStartupItemPriority => RequestLifeCycleStartupItemPriority.seedingData;

        bool IRequestLifeCycleStartupItem.Execute()
        {
            this.addSoftwareTypes().Wait();
            this.addVendors().Wait();

            return true;
        }

        private async Task addSoftwareTypes()
        {
            var softwareTypeValues = Enum.GetValues(typeof(SoftwareTypeEnum));
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        foreach (int softwareTypeValue in softwareTypeValues)
                        {
                            var name = Enum.GetName(typeof(SoftwareTypeEnum), softwareTypeValue);
                            if (name == null)
                            {
                                continue;
                            }

                            var softwareType = await context.SoftwareType.SingleOrDefaultAsync(x => x.Id == softwareTypeValue);
                            if (softwareType != null)
                            {
                                continue;
                            }

                            context.SoftwareType.Add(
                                new SoftwareType
                                    {
                                        Id = softwareTypeValue,
                                        Name = name
                                    });
                        }

                        return WorkItemResultEnum.commitSuccessfullyCompleted;
                    });
            await uow.ExecuteAsync();
        }

        private async Task addVendors()
        {
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var softwareTypes = context.SoftwareType.ToList();
                        foreach (var softwareType in softwareTypes)
                        {
                            var vendor = await context.Vendor.SingleOrDefaultAsync(x => x.SoftwareTypeId == softwareType.Id);
                            if (vendor != null)
                            {
                                continue;
                            }

                            context.Vendor.Add(
                                new Vendor
                                    {
                                        Id = softwareType.Id,
                                        CreatedOn = DateTime.Now,
                                        ModifiedOn = DateTime.Now,
                                        IsActive = true,
                                        Name = softwareType.Name,
                                        SoftwareTypeId = softwareType.Id
                                    });
                        }

                        return WorkItemResultEnum.commitSuccessfullyCompleted;
                    });
            await uow.ExecuteAsync();
        }
    }
}