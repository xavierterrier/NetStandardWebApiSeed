using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Database
{
    public interface IDbProvider
    {
        ModelContext GetModelContext();
    }
}