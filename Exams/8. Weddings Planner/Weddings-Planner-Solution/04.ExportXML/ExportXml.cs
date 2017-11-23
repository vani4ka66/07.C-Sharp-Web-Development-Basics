using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WeddingsPlanner.Data;
using WeddingsPlanner.Models;

namespace _04.ExportXML
{
    class ExportXml
    {
        static void Main(string[] args)
        {
            ExportVenuesInSofia();
            ExportAgenciesByLocation();
        }

        private static void ExportAgenciesByLocation()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {

                var towns = uow.Agencies.GetAll()
                    .GroupBy(a => a.Town, a => a, (town, agencies) => new
                    {
                        Town = town,
                        Agencies = agencies.Where(a => a.OrganizedWeddings.Count() > 1)
                    })
                    .Where(t => t.Town.Length > 5);

                var xml = new XElement("towns");
                foreach (var town in towns)
                {
                    var townNode = new XElement("town");
                    townNode.Add(new XAttribute("name", town.Town));
                    var agenciesNode = new XElement("agencies");
                    foreach (var agency in town.Agencies)
                    {
                        var agencyNode = new XElement("agency");
                        agencyNode.Add(new XAttribute("name", agency.Name));
                        agencyNode.Add(new XAttribute("profit", CalculateProfit(agency.OrganizedWeddings)));
                        foreach (var wedding in agency.OrganizedWeddings)
                        {
                            var weddingNode = new XElement("wedding");
                            weddingNode.Add(new XAttribute("cash", CalculateWeddingCash(wedding.Invitations)));
                            weddingNode.Add(new XAttribute("presents", GetPresentsCount(wedding.Invitations)));
                            weddingNode.Add(new XElement("bride", wedding.Bride.FullName));
                            weddingNode.Add(new XElement("bridegroom", wedding.Bridegroom.FullName));
                            var guestsNode = new XElement("guests");
                            foreach (var guest in wedding.Invitations)
                            {
                                var guestNode = new XElement("guest", guest.Guest.FullName);
                                guestNode.Add(new XAttribute("family", guest.Family));
                                guestsNode.Add(guestNode);
                            }
                            weddingNode.Add(guestsNode);
                            agencyNode.Add(weddingNode);
                        }
                        agenciesNode.Add(agencyNode);
                    }
                    townNode.Add(agenciesNode);
                    xml.Add(townNode);
                }
                xml.Save("../../agencies-by-location.xml");
            }
        }

        private static decimal CalculateProfit(ICollection<Wedding> organizedWeddings)
        {
            return organizedWeddings
                .Sum(w => w.Invitations
                .Where(i => (i.Present as Cash) != null)
                    .Sum(i => (i.Present as Cash).Amount)
                ) * 0.2m;
        }

        private static int GetPresentsCount(ICollection<Invitation> invitations)
        {
            return invitations.Where(i => (i.Present as Gift) != null)
                .Count();
        }

        private static decimal CalculateWeddingCash(ICollection<Invitation> invitations)
        {
            return invitations.Where(i => (i.Present as Cash) != null)
                .Sum(i => (i.Present as Cash).Amount);
        }

        private static void ExportVenuesInSofia()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {
                var venues = uow.Venues.GetAll()
                    .Where(v => v.Weddings.Count >= 3 && v.Town == "Sofia")
                    .OrderBy(v => v.Capacity);
                var xml = new XElement("venues");
                xml.Add(new XAttribute("town", "Sofia"));
                foreach (var venue in venues)
                {
                    var venueNode = new XElement("venue");
                    venueNode.Add(new XAttribute("name", venue.Name));
                    venueNode.Add(new XAttribute("capacity", venue.Capacity));
                    venueNode.Add(new XElement("weddings-count", venue.Weddings.Count));
                    xml.Add(venueNode);
                }

                xml.Save("../../sofia-venues.xml");
            }
        }
    }
}
