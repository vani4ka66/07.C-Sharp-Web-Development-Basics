namespace MassDeffect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public Planet()
        {
            this.People = new List<Person>();
            this.OriginOfAnomalies = new HashSet<Anomaly>();
            this.TeleportOfAnomalies = new List<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Star Sun { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Anomaly> OriginOfAnomalies { get; set; }

        public virtual ICollection<Anomaly> TeleportOfAnomalies { get; set; }
    }
}
