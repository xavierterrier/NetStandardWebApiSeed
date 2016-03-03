using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using WebAPIToolkit.Common.Authentication;
using WebAPIToolkit.Model;
using WebAPIToolkit.Model.Database;

namespace WebAPIToolkit.Common
{
    /// <summary>
    /// The IoC (Inversion of Control)
    /// See Dependency injection pattern https://en.wikipedia.org/wiki/Dependency_injection
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        /// <summary>
        /// The container
        /// </summary>
        private static readonly IUnityContainer Container;

        private bool _disposed;

        static UnityResolver()
        {
            Container = new UnityContainer();
        }

        /// <summary>
        /// Inject real storage items
        /// </summary>
        public static void Initialize()
        {
            Container.RegisterType<IDbProvider, DbProvider>();
            Container.RegisterType<IUserStore<User, int>, EntityFrameworkUserStore>();
            Container.RegisterType<IRoleStore<Role, int>, EntityFrameworkRoleStore>();
            

            //Container.RegisterType<ILogger>(new ContainerControlledLifetimeManager(),
            //   new InjectionFactory(l => new Logger.Logger(connectionString, databaseName, "Logs", loggerName)));
        }

        /// <summary>
        /// Inject MOQs
        /// </summary>
        public static void UnitTestInitialize()
        {
            Container.RegisterType<IDbProvider, FakeDbProvider>();
            Container.RegisterType<IUserStore<User, int>, EntityFrameworkUserStore>();
            Container.RegisterType<IRoleStore<Role, int>, EntityFrameworkRoleStore>();
        }

        /// <summary>
        /// Creates a new nested UnityContainer as a child of the current container. The current container first applies its own settings, and then it checks the parent for additional settings. Returns a reference to the new container
        /// </summary>
        /// <returns></returns>
        public static IUnityContainer CreateChildContainer()
        {
            return Container.CreateChildContainer();
        }

        /// <summary>
        /// Resolves singly registered type that Support arbitrary object creation 
        /// </summary>
        /// <typeparam name="T">The type of the requested service or object.</typeparam>
        /// <returns>The requested service or object.</returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Resolves singly registered type that Support arbitrary object creation
        /// </summary>
        /// <param name="t">The dependency resolver instance that this method extends.</param>
        /// <returns>The requested service or object.</returns>
        public static object Resolve(Type t)
        {
            return Container.Resolve(t);
        }

        /// <summary>
        /// Resolves singly registered services that Support arbitrary object creation
        /// </summary>
        /// <param name="serviceType">The dependency resolver instance that this method extends.</param>
        /// <returns>The requested service or object.</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <returns></returns>
        [Obsolete("See https://msdn.microsoft.com/en-us/library/microsoft.practices.unity.unitycontainer.createchildcontainer.aspx")]
        public IDependencyScope BeginScope()
        {
            // var child = Container.CreateChildContainer();
            return new UnityResolver();
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers 
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            // Suppress finalization.
            GC.SuppressFinalize(this);

            _disposed = true;
        }
    }
}