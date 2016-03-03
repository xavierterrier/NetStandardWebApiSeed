using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebAPIToolkit.Model;
using WebAPIToolkit.Model.Database;

namespace WebAPIToolkit.Common.Authentication
{
    /// <summary>
    /// This is a basic implementation of UserStore used by EntityFramework
    /// </summary>
    public class EntityFrameworkUserStore : IUserPasswordStore<User, int>, IUserRoleStore<User, int>
    {
        private readonly IDbProvider _dbProvider;
        private bool _disposed;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbProvider"></param>
        public EntityFrameworkUserStore(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        /// <summary>
        /// Asynchronously inserts a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateAsync(User user)
        {
            using (var db = _dbProvider.GetModelContext())
            {
               // user.Id = Guid.NewGuid().ToString();
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Asynchronously updates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateAsync(User user)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }

        }

        /// <summary>
        /// Asynchronously deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task DeleteAsync(User user)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Deleted;

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Asynchronously finds a user using the specified identifier
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(int userId)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                return await db.Users.Include(u => u.Roles).SingleOrDefaultAsync(u=> u.Id == userId);
            }

        }

        /// <summary>
        /// Asynchronously finds a user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User> FindByNameAsync(string userName)
        {
            using (var db = _dbProvider.GetModelContext())
            {

                // var users = db.Users.Where(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                var users = from u in db.Users
                            where u.UserName == userName
                            select u;

                return await users.Include(u => u.Roles).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Asynchronously sets the user password hash
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously gets the user password hash
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// Indicates whether the user has a password set
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(User user)
        {
            var hasPassword = String.IsNullOrEmpty(user.PasswordHash);
            return Task.FromResult(hasPassword);
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

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            IList<string> result = user.Roles?.Select(r => r.Name).ToList() ?? new List<string>();

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var result = user.Roles?.Any(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase)) ?? false;

            return Task.FromResult(result);
        }
    }
}
