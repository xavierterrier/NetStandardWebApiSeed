using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
    }
}