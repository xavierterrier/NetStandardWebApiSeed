using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Dtos
{
    public class UserInfoDto
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}