using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPIToolkit.Model
{
    /// <summary>
    /// A basic role
    /// </summary>
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}