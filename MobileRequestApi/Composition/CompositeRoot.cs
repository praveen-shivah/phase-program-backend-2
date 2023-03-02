namespace MobileRequestApi
{
    using ApplicationLifeCycle;

    using AuthenticationRepository;

    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.Extensions.Configuration;

    using SharedUtilities;

    using SimpleInjector;

    using System;
    using System.IO;
    using System.Xml;

    using AutomaticTaskSharedLibrary;

    using InvoiceRepositoryTypes;

    using RestServicesSupport;

    using RestServicesSupportTypes;

    using SecurityUtilities;

    using SecurityUtilitiesTypes;

    using UnitOfWorkTypesLibrary;

    using LoggerAdapterFactory = LoggingServicesLibrary.LoggerAdapterFactory;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            var environment = this.getEnvironmentName();

            Console.WriteLine($"*********************  environment {environment}");

            this.GlobalContainer.RegisterInstance(typeof(IConfiguration), this.buildConfig(environment));

            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ISecretKeyRetrieval, SecretKeyRetrievalSettingsFile>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IConnectionFactory, ConnectionFactoryNormal>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IEntityContextFrameWorkFactory<DataContext>, EntityContextFrameWorkFactoryNormal>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IRestServicesFactory<DistributorToResellerSendPointsTransferRequestDto, DistributorToOperatorSendPointsTransferResponseDto>, RestServicesFactory<DistributorToResellerSendPointsTransferRequestDto, DistributorToOperatorSendPointsTransferResponseDto>>();
            this.GlobalContainer.Register<IRestServicesFactory<ResellerBalanceRetrieveRequestDto, ResellerBalanceRetrieveResponseDto>, RestServicesFactory<ResellerBalanceRetrieveRequestDto, ResellerBalanceRetrieveResponseDto>>();

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
