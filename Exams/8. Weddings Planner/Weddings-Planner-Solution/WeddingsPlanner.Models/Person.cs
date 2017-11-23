namespace WeddingsPlanner.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Person
    {
        public Person()
        {
            this.Invitations = new HashSet<Invitation>();
            this.Bridegrooms = new HashSet<Wedding>();
            this.Brides = new HashSet<Wedding>();
        }
        public int Id { get; set; }
        [Required, StringLength(60, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required, StringLength(1, MinimumLength = 1)]
        public string MiddleNameInitial { get; set; }
        [Required, MinLength(2)]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.MiddleNameInitial} {this.LastName}";
            }
        }
        public Gender Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        [NotMapped]
        public int? Age
        {
            get
            {
                if (Birthdate == null) return null;
                var now = DateTime.Now;
                int age = now.Year - ((DateTime)this.Birthdate).Year;

                if (now.Month < ((DateTime)Birthdate).Month ||
                    (now.Month == ((DateTime)Birthdate).Month && now.Day < ((DateTime)Birthdate).Day))
                {
                    age--;
                }

                return age;
            }
        }
        public string Phone { get; set; }
        [RegularExpression(@"[a-zA-Z0-9]+@[a-z]{1,}.[a-z]{1,}")]
        public string Email { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
        [InverseProperty("Bride")]
        public virtual ICollection<Wedding> Brides { get; set; }
        [InverseProperty("Bridegroom")]
        public virtual ICollection<Wedding> Bridegrooms { get; set; }
    }
}
