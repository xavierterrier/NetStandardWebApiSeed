using Microsoft.Practices.Unity;

namespace WebAPIToolkit.Common
{
    public static class IoC
    {
        private static IUnityContainer _container;

        static IoC()
        {
            _container = new UnityContainer();
            Initialize();
        }

        public static void Initialize()
        {


            //_container.RegisterType<ILogger>(new ContainerControlledLifetimeManager(),
            //   new InjectionFactory(l => new Logger.Logger(connectionString, databaseName, "Logs", loggerName)));

            //_container.RegisterType<MailTools>(new ContainerControlledLifetimeManager(),
            //   new InjectionFactory(l => new MailTools(SmtpSettings.Settings.Host, SmtpSettings.Settings.Port, SmtpSettings.Settings.UseSSL, SmtpSettings.Settings.Username, SmtpSettings.Settings.Password)));
        }



        private static IUnityContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}