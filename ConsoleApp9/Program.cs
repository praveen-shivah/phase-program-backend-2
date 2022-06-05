// See https://aka.ms/new-console-template for more information

using System.Reflection;

using AutomaticTaskLibrary;

using ConsoleApp9;

using log4net;
using log4net.Config;

using SimpleInjector.Lifestyles;

Console.WriteLine("Test automation api");
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("ConsoleAoo");
applicationLifeCycle.GlobalContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
applicationLifeCycle.Initialize();
var response = applicationLifeCycle.StartRequest();

var loggerFactory = applicationLifeCycle.Resolve<LoggingLibrary.ILoggerFactory>();
var logger = loggerFactory.Create("HostingApplicationService");

// var test = applicationLifeCycle.Resolve<IDistributorToResellerSendPointsTransferTest>();
var test = applicationLifeCycle.Resolve<IResellerBalanceRetrieveTest>();
test.RunTest();

Console.WriteLine("Hit any key to continue");
Console.ReadKey();
