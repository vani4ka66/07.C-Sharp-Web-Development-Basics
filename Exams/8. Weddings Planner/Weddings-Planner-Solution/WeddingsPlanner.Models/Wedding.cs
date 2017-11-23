using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Models
{
    public class Wedding
    {
        public Wedding()
        {
            this.Venues = new HashSet<Venue>();
            this.Invitations = new HashSet<Invitation>();
        }
        public int Id { get; set; }

        public int? BrideId { get; set; }
        public int? BridegroomId { get; set; }
        [ForeignKey("BrideId")]
        public virtual Person Bride { get; set; }

        [ForeignKey("BridegroomId")]
        public virtual Person Bridegroom { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Venue> Venues { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
