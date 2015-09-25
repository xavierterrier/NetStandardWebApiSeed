using System.Data.Entity;

namespace WebAPIToolkit.Models
{
    public class ModelContext : DbContext
    {

        public ModelContext() : base("ModelContextDatabase")
        {
            
        }


        public DbSet<User>  Users { get; set; }
    }
}