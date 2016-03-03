using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace WebAPIToolkit.Model
{
    /// <summary>
    /// A basic role
    /// </summary>
    public class Role : IRole<int>
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}