using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssuTracker_App.Models.Enums;

namespace IssuTracker_App.Models
{
    public class Issue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        [ForeignKey("Author")]
        public int UserId { get; set; }

        public User Author { get; set; }

    }
}
