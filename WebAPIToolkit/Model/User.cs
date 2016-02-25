using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebAPIToolkit.Model
{
    /// <summary>
    /// A basic user...    
    /// </summary>
    public class User : IUser<int> 
    {
        [Key]
        public int Id { get; set; }
       
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
