using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01.MassDefect_CodeFirst.Models
{
    [Table("Persons")]
    public class Person
    {
        public Person()
        {
            this.Anomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? HomePlanetId { get; set; }

        [ForeignKey("HomePlanetId")]
        public virtual Planet HomePlanet { get; set; }

        public virtual HashSet<Anomaly> Anomalies { get; set; }
    }
}
