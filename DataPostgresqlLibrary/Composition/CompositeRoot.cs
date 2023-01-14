﻿namespace DataPostgresqlLibrary
{
    using System;
    using System.Xml;

    using ApplicationLifeCycle;

    using Microsoft.Extensions.Configuration;

    using SimpleInjector;

    using UnitOfWorkClassLibrary;

    using UnitOfWorkTypesLibrary;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IUnitOfWorkFactory<DPContext>, UnitOfWorkFactory<DPContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IUnitOfWorkContextContainerFactory<DPContext>, UnitOfWorkContextContainerFactory<DPContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IWorkItemFactory<DPContext>, WorkItemFactory<DPContext>>(Lifestyle.Singleton);

            return true;
        }
    }
}
