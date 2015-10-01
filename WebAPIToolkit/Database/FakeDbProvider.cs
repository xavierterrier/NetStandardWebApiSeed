using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Database
{
    // Use Effort (https://effort.codeplex.com/wikipage?title=Tutorials&referringTitle=Home) to simulate database
    public class FakeDbProvider : IDbProvider
    {

        private static readonly DbConnection Context = Effort.DbConnectionFactory.CreateTransient();

        static FakeDbProvider()
        {
            Context = Effort.DbConnectionFactory.CreateTransient();
        }

        


        public ModelContext GetModelContext()
        {
            return new ModelContext(Context);
        }
    }
}