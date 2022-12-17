namespace DataPostgresqlLibrary
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
            var environment = this.getEnvironmentName();

            var output = $"*********************  Environment: {environment} *********************";

            Console.WriteLine("".PadRight(output.Length, '*'));
            Console.WriteLine(output);
            Console.WriteLine("".PadRight(output.Length, '*'));

            this.GlobalContainer.Register<IUnitOfWorkFactory<DPContext>, UnitOfWorkFactory<DPContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IUnitOfWorkContextContainerFactory<DPContext>, UnitOfWorkContextContainerFactory<DPContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IWorkItemFactory<DPContext>, WorkItemFactory<DPContext>>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterInstance(typeof(IConfiguration), this.buildConfig(environment));

            return true;
        }

        private string getEnvironmentName()
        {
            if (!File.Exists("web.config"))
            {
                return "Development";
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("web.config");

            if (doc.DocumentElement == null)
            {
                return "Development";
            }

            XmlElement root = doc.DocumentElement;
            var node = root.SelectSingleNode("//environmentVariables");
            if (node == null)
            {
                return "Development";
            }

            var env = node["environmentVariable"];
            if (env == null)
            {
                return "Development";
            }

            return env.Attributes[1].Value;
        }

        private IConfiguration buildConfig(string environment)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile($"appsettings.{environment}.json", true);
            IConfiguration config = builder.Build();
            return config;
        }
    }
}
