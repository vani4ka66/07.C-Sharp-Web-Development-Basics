using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshop.Models
{
    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Lens>();
            this.Accessories = new HashSet<Accessory>();
            this.WorkshopsParticipated = new HashSet<Workshop>();
            this.WorkshopsTrained = new HashSet<Workshop>();
        }
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [RegularExpression(@"\+\d{1,3}\/\d{8,10}")]
        public string Phone { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{this.FirstName} {this.LastName}"; }
        }

        public int? PrimaryCameraId { get; set; }
        public int? SecondaryCameraId { get; set; }

        [ForeignKey("PrimaryCameraId")]
        public virtual Camera PrimaryCamera { get; set; }
        [ForeignKey("SecondaryCameraId")]
        public virtual Camera SecondaryCamera { get; set; }
        public virtual ICollection<Lens> Lenses { get; set; }
        public virtual ICollection<Accessory> Accessories { get; set; }
        public virtual ICollection<Workshop> WorkshopsParticipated { get; set; }
        public virtual ICollection<Workshop> WorkshopsTrained { get; set; }
    }
}