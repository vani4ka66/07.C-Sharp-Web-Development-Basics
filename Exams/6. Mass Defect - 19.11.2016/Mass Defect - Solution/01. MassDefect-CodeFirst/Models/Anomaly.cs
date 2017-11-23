using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01.MassDefect_CodeFirst.Models
{
    public class Anomaly
    {
        public Anomaly()
        {
            this.Victims = new HashSet<Person>();
        }

        public int Id { get; set; }

        public int? OriginPlanetId { get; set; }

        public int? TeleportPlanetId { get; set; }

        [ForeignKey("OriginPlanetId")]
        [InverseProperty("OriginAnomalies")]
        public virtual Planet OriginPlanet { get; set; }

        [ForeignKey("TeleportPlanetId")]
        public virtual Planet TeleportPlanet { get; set; }

        public HashSet<Person> Victims { get; set; }
    }
}
