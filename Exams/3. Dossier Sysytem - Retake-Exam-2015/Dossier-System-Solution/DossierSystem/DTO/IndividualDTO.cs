namespace DossierSystem.DTO
{
    using System;
    using System.Collections.Generic;
    using Models;

    public class IndividualDTO
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Alias { get; set; }

        public DateTime? BirthDate { get; set; }

        public Status Status { get; set; }

        public IEnumerable<ActivityDTO> Activities { get; set; }

        public IEnumerable<LocationDTO> Locations { get; set; }
    }

    public class ActivityDTO
    {
        public string ActivityType { get; set; }

        public string Description { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime? ActiveTo { get; set; }
    }

    public class LocationDTO
    {
        public string City { get; set; }

        public DateTime LastSeen { get; set; }
    }
}
