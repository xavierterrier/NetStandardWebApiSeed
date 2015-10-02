using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Dtos
{
    public class ExternalLoginProviderDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }
}