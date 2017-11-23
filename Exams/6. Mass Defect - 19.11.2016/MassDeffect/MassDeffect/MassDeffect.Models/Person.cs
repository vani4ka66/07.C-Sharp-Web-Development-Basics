namespace MassDeffect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Persons")]
    public class Person
    {
        public Person()
        {
            this.Anomalies = new List<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Planet HomePlanet { get; set; }

        public virtual ICollection<Anomaly> Anomalies { get; set; }

    }
}
