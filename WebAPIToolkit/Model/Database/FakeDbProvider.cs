using System.Data.Common;

namespace WebAPIToolkit.Model.Database
{
    /// <summary>
    /// Use Effort (https://effort.codeplex.com/wikipage?title=Tutorials&referringTitle=Home) to simulate a database
    /// </summary>
    public class FakeDbProvider : IDbProvider
    {

        private static readonly DbConnection Context;

        static FakeDbProvider()
        {
            Context = Effort.DbConnectionFactory.CreateTransient();
        }

        /// <summary>
        /// Get ApplicationContext
        /// </summary>
        /// <returns></returns>
        public ApplicationContext GetModelContext()
        {
            return new ApplicationContext(Context);
        }
    }
}