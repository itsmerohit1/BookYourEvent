using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Models
{
    public class Comment
    {
       
        [Required]
        [Display(Name ="Comment")]
        public string CommentAdded { get; set; }


       

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Key]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string Time { get; set; }

        public string EventId { get; set; }

         
        public string EmailId { get; set; }
    }
}