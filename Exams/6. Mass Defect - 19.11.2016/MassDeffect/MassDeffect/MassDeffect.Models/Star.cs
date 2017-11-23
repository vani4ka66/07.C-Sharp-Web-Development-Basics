namespace MassDeffect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Star
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }
    }
}
