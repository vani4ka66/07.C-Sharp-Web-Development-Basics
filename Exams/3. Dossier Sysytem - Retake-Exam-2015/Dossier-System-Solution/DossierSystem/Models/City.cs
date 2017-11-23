namespace DossierSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
