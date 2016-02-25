using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPIToolkit.Model
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }

        public string Contact { get; set; }

        public DateTime? DueDate { get; set; }

        public string Manager { get; set; }

        public string Name { get; set; }

        public Project Project { get; set; }

        public Enums.State State { get; set; }

    }
}