using System;
using System.Collections.Generic;
using System.Text;
namespace Shared
{
    public class Invitation
    {
        public int InvitationId { get; set; }
        public string Email { get; set; }
        public int EventId { get; set; }
        public virtual Events Event { get; set; }


    }
}
