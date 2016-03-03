using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebAPIToolkit.Model;
using WebAPIToolkit.Model.Database;

namespace WebAPIToolkit.Common.Authentication
{
    /// <summary>
    /// Role store
    /// </summary>
    public class EntityFrameworkRoleStore : IRoleStore<Role, int>
    {
        private readonly IDbProvider _dbProvider;
        private bool _disposed;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbProvider"></param>
        public EntityFrameworkRoleStore(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task CreateAsync(Role role)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                // user.Id = Guid.NewGuid().ToString();
                db.Roles.Add(role);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Role role)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                db.Roles.Attach(role);
                db.Entry(role).State = EntityState.Deleted;

                await db.SaveChangesAsync();
            }
        }


        public async Task<Role> FindByIdAsync(int roleId)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                return await db.Roles.FindAsync(roleId);
            }
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                var roles = from u in db.Roles
                            where u.Name == roleName
                            select u;

                return await roles.FirstOrDefaultAsync();
            }
        }

        public async Task UpdateAsync(Role role)
        {
            using (var db = _dbProvider.GetModelContext())
            {
                db.Roles.Attach(role);
                db.Entry(role).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }
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