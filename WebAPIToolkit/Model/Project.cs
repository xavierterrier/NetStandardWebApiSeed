using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPIToolkit.Model
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public Enums.Practice Practice { get; set; }

        public string Manager { get; set; }

        //@Column(name = "MANAGER", nullable = false)
        //@Getter @Setter private String manager;

        public string PersonInCharge { get; set; }

        public string Contact { get; set; }

        public Enums.Status Status { get; set; }

        public string Phase { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}