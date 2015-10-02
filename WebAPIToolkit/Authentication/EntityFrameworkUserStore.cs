using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebAPIToolkit.Database;
using WebAPIToolkit.Models;

namespace WebAPIToolkit.Authentication
{
    /// <summary>
    /// This is a basic implementation of UserStore that use EntityFramework
    /// </summary>
    public class EntityFrameworkUserStore : IUserStore<User, int>, IUserPasswordStore<User, int>//, IUserLoginStore<User>
    {
        private readonly IDbProvider _dbProvider;


        public EntityFrameworkUserStore(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public void Dispose()
        {

        }

        public async Task CreateAsync(User user)
        {
            using (var db = _dbProvider.GetModelContext())
            {
               // user.Id = Guid.NewGuid().ToString();
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }

        }

        public async Task DeleteAsync(User user)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Deleted;

                await db.SaveChangesAsync();
            }
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                return await db.Users.FindAsync(userId);
            }

        }

        public async Task<User> FindByNameAsync(string userName)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                var users = from u in db.Users
                            where u.UserName == userName
                            select u;

                return await users.FirstOrDefaultAsync();
            }
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            var hasPassword = String.IsNullOrEmpty(user.PasswordHash);
            return Task.FromResult(hasPassword);
        }
    }
}
