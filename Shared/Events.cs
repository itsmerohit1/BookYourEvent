using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;



namespace Shared
{



    public class Events
    {
      
 
        [Key]
        public string Title { get; set; }      

        public DateTime Date { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public EventType? Type{ get; set; }
        public float? Duration { get; set; }
        public string Description { get; set; }
        public string OtherDetails { get; set; }
        public string InviteByEmail { get; set; }
       

        public string EmailId { get; set; }
     
       

        public virtual ICollection<Invitation> Invitations { get; set; }
        
    }
}
