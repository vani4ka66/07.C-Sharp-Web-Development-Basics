using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssuTracker_App.Models.Enums;

namespace IssuTracker_App.Models
{
    public class User
    {
        public User()
        {
            this.Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Issue> Issues{ get; set; }
    }
}
