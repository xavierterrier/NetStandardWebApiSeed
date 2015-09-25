using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Dtos
{
    public class AuthTokenDto
    {
        public string AccessToken { get; set; }

        public string UserName { get; set; }
    }
}