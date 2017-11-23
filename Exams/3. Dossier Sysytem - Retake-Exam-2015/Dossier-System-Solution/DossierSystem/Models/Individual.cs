namespace DossierSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Individual
    {
        // Vasil Iliev, Ivo Karamanski, Poli Pantev, Bay Mile, 
        // Dimata Rusnaka, 
        private ICollection<Activity> activities;
        private ICollection<Location> locations;
        private ICollection<Individual> related;

        public Individual()
        {
            this.activities = new HashSet<Activity>();
            this.locations = new HashSet<Location>();
            this.related = new HashSet<Individual>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Alias { get; set; }

        public DateTime? BirthDate { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<Activity> Activities
        {
            get { return this.activities; }
            set { this.activities = value; }
        }

        public virtual ICollection<Location> Locations
        {
            get { return this.locations; }
            set { this.locations = value; }
        }

        public virtual ICollection<Individual> Related
        {
            get { return this.related; }
            set { this.related = value; }
        }
    }
}
