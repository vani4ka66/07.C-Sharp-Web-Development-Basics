using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore_App.Models
{
    public class User
    {
        public User()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        //[Index(IsUnique = true)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
