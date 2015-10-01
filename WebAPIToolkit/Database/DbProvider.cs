using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;

namespace WebAPIToolkit.Database
{
    public class DbProvider : IDbProvider
    {
        public ModelContext GetModelContext()
        {
            return new ModelContext();
        }
    }
}