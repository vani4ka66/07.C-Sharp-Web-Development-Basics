using WeddingsPlanner.Models.Enums;

namespace _01.ImportJSON.DTOs
{
    class GuestDto
    {
        public string Name { get; set; }
        public bool RSVP { get; set; }
        public Family Family { get; set; }
    }
}
