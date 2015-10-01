using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using WebAPIToolkit.Authentication;
using WebAPIToolkit.Database;
using WebAPIToolkit.Models;

namespace WebAPIToolkit.Common
{
    public static class IoC //TODO: Move this to UnityResolver or App_Start
    {
        private static IUnityContainer _container;

        static IoC()
        {
            _container = new UnityContainer();
        }

        public static void Initialize()
        {
            _container.RegisterType<IDbProvider, DbProvider>();
            _container.RegisterType<IUserStore<User, int>, EntityFrameworkUserStore>();

            //_container.RegisterType<ILogger>(new ContainerControlledLifetimeManager(),
            //   new InjectionFactory(l => new Logger.Logger(connectionString, databaseName, "Logs", loggerName)));
        }

        public static void UnitTestInitialize()
        {
            _container.RegisterType<IDbProvider, FakeDbProvider>();
            _container.RegisterType<IUserStore<User, int>, EntityFrameworkUserStore>();
        }



        public static IUnityContainer Container
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

        public static IUnityContainer CreateChildContainer()
        {
            return _container.CreateChildContainer();
        }


        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static object Resolve(Type t)
        {
            return _container.Resolve(t);
        }
    }
}