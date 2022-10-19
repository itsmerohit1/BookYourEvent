using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared
{
    public class User
    {

        public static string UserName { get; set; }

        [Key]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Events> Events { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
