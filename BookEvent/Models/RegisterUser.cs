using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Models
{
    public class RegisterUser
    {
       
        [Required]
        public string UserName { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
