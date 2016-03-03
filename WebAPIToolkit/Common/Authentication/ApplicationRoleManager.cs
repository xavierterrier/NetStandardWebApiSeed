using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebAPIToolkit.Model;

namespace WebAPIToolkit.Common.Authentication
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        /// <summary>
        /// The default constructor wich used IoC to retrieved UserStore
        /// </summary>
        public ApplicationRoleManager() : base(UnityResolver.Resolve<IRoleStore<Role, int>>())
        {

        }

        public ApplicationRoleManager(IRoleStore<Role, int> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var manager = new ApplicationRoleManager(UnityResolver.Resolve<IRoleStore<Role, int>>());
            manager.RoleValidator = new RoleValidator<Role, int>(manager);

            return manager;
        }
    }
}