namespace DossierSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Activity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }

        [Required]
        [ForeignKey("Individual")]
        public string IndividualId { get; set; }

        public virtual Individual Individual { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }
    }
}
