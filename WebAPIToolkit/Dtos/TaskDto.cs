using System;
using WebAPIToolkit.Model;

namespace WebAPIToolkit.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Contact { get; set; }

        public DateTime? DueDate { get; set; }

        public string Manager { get; set; }

        public string Name { get; set; }

        public int? ProjectId { get; set; }

        public Enums.State State { get; set; }
    }
}