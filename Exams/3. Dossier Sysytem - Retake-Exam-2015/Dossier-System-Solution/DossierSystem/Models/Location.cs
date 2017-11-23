namespace DossierSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Location
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string IndividualId { get; set; }

        public virtual Individual Individual { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
