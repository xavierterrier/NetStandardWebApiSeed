using System.Data.Common;
using System.Data.Entity;
using WebAPIToolkit.Common;

namespace WebAPIToolkit.Model.Database
{
    /// <summary>
    /// The EF application context.
    /// This is the main class that coordinates Entity Framework functionality for a given data model.
    /// </summary>
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base("ModelContextDatabase") // Web.config must contain a connectionstring "ModelContextDatabase"
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


        /// <summary>
        /// The users set
        /// </summary>
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}