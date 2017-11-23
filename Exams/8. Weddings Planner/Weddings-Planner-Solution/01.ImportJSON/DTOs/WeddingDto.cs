using System;
using System.Collections.Generic;

namespace _01.ImportJSON.DTOs
{
    class WeddingDto
    {
        public string Bride { get; set; }
        public string Bridegroom { get; set; }
        public DateTime Date { get; set; }
        public string Agency { get; set; }
        public ICollection<GuestDto> Guests { get; set; }
    }
}
