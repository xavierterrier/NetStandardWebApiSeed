using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebAPIToolkit.Models;

namespace WebAPIToolkit.Authentication
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(UserStore store)
            : base(store)
        {

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {

            var manager = new ApplicationUserManager(new WebAPIToolkit.Authentication.UserStore()); // TODO: Ioc
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
