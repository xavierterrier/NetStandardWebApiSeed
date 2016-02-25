using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebAPIToolkit.Model;

namespace WebAPIToolkit.Common.Authentication
{
    /// <summary>
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class ApplicationUserManager : UserManager<User, int>
    {
        /// <summary>
        /// The default constructor wich used IoC to retrieved UserStore
        /// </summary>
        public ApplicationUserManager() : base(UnityResolver.Resolve<IUserStore<User, int>>())
        {
            
        }

        /// <summary>
        /// Constructor using a given UserStore
        /// </summary>
        /// <param name="store"></param>
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
            
        }

        /// <summary>
        /// Creates an ApplicationUserManager with default settings
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {            
            var manager = new ApplicationUserManager(UnityResolver.Resolve<IUserStore<User, int>>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            //  manager.UserTokenProvider = new UserTokenProvider<User>();
            return manager;
        }

        //public class UserValidator : IIdentityValidator<User>
        //{
        //    public async Task<IdentityResult> ValidateAsync(User item)
        //    {
        //        if (string.IsNullOrWhiteSpace(item.UserName))
        //        {
        //            return IdentityResult.Failed("Really?!");
        //        }

        //        return IdentityResult.Success;
        //    }
        //}
    }
}
