using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Models
{
    public class LoginUser
    {
        [Key]
        [Required]
        public string EmailId{ get; set; }

        [Required]
        public string Password { get; set; }
    }
}
