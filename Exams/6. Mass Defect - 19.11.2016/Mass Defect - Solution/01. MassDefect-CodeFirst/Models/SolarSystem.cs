using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _01.MassDefect_CodeFirst.Models
{
    public class SolarSystem
    {
        public SolarSystem()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
        }
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual HashSet<Star> Stars { get; set; }

        public virtual HashSet<Planet> Planets { get; set; }
    }
}
