using WeddingsPlanner.Models.Enums;

namespace _02.ImportXML.DTOs
{
    class GiftDto
    {
        public string Type { get; set; }
        public int WeddingId { get; set; }
        public string GuestName { get; set; }
        public string PresentName { get; set; }
        public PresentSize Size { get; set; }
    }
}
