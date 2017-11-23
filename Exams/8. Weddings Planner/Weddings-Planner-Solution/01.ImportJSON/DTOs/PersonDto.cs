using System;
using WeddingsPlanner.Models.Enums;

namespace _01.ImportJSON.DTOs
{
    class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
