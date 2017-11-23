using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01.MassDefect_CodeFirst.Models
{
    public class Planet
    {
        public Planet()
        {
            this.Persons = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public int? SunId { get; set; }

        public int? SolarSystemId { get; set; }

        [ForeignKey("SunId")]
        public virtual Star Sun { get; set; }

        [ForeignKey("SolarSystemId")]
        public virtual SolarSystem SolarSystem { get; set; }

        public virtual HashSet<Person> Persons { get; set; }

        public virtual HashSet<Anomaly> OriginAnomalies { get; set; }

        public virtual HashSet<Anomaly> TeleportAnomalies { get; set; }
    }
}
