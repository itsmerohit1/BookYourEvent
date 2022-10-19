using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEvent.Models
{
    public class Event
    {
		[Key]

		[Display(Name = "Title")]
		[Required(ErrorMessage ="This Field can't be empty")]
		public string Title { get; set; }

		[Display(Name = "Date")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "This Field can't be empty")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public string Date { get; set; }

		[Display(Name = "Location")]
		[Required(ErrorMessage = "This Field can't be empty")]
		public string  Location { get; set; }

		[Display(Name = "Start Time")]
	 
		[DataType(DataType.Time)]
		[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
		public string StartTime { get; set; }


		public EventType? Type{ get; set; }


		[Display(Name = "Duration In Hours")]
		[Range(0, 4)]
		public string Duration{ get; set; }

		[MaxLength(50)]
		public string Description { get; set; }

		[MaxLength(500)]
		[Display(Name = "Other Details")]
		public string OtherDetails { get; set; }

		[Display(Name = "Invite Others")]
		public string InviteByEmail { get; set; }

		 

	}

}
