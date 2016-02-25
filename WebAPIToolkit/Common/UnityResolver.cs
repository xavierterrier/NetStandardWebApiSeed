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
    public class UnityResolver : IDependencyResolver
    {
        private static IUnityContainer _container;

        static UnityResolver()
        {
            _container = new UnityContainer();
        }

        /// <summary>
        /// Inject real storage items
        /// </summary>
        public static void Initialize()
        {
            _container.RegisterType<IDbProvider, DbProvider>();
            _container.RegisterType<IUserStore<User, int>, EntityFrameworkUserStore>();

            //_container.RegisterType<ILogger>(new ContainerControlledLifetimeManager(),
            //   new InjectionFactory(l => new Logger.Logger(connectionString, databaseName, "Logs", loggerName)));
        }

        /// <summary>
        /// Inject MOQs
        /// </summary>
        public static void UnitTestInitialize()
        {
            _container.RegisterType<IDbProvider, FakeDbProvider>();
            _container.RegisterType<IUserStore<User, int>, EntityFrameworkUserStore>();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        [Obsolete("See https://msdn.microsoft.com/en-us/library/microsoft.practices.unity.unitycontainer.createchildcontainer.aspx")]
        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver();
        }

        public void Dispose()
        {
            
        }
    }
}