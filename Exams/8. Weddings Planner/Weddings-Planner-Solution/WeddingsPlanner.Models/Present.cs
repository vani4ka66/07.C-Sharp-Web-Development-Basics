﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Models
{
    public class Present
    {
        public int InvitationId { get; set; }
        [NotMapped]
        public Person Owner { get { return this.Invitation.Guest; } }
        public virtual Invitation Invitation { get; set; }
    }
}
