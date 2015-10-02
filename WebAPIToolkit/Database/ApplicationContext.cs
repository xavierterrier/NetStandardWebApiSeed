using System.Data.Common;
using System.Data.Entity;
using WebAPIToolkit.Common;
using WebAPIToolkit.Models;

namespace WebAPIToolkit.Database
{
    /// <summary>
    /// The EF application context
    /// </summary>
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base("ModelContextDatabase")
        {

        }

        public ApplicationContext(DbConnection connection) : base(connection, true)
        {
            
        }

        public static ApplicationContext Create()
        {
            var dbProvider = UnityResolver.Resolve<IDbProvider>();

            return dbProvider.GetModelContext();
        }


        public DbSet<User> Users { get; set; }
    }
}