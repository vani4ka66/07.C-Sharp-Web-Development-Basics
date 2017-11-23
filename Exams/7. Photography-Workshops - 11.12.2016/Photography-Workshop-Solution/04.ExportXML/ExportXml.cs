using PhotographyWorkshop.Data;
using PhotographyWorkshop.Models;
using System.Linq;
using System.Xml.Linq;

namespace _04.ExportXML
{
    class ExportXml
    {
        static void Main()
        {
            PhotographersWithSameCameraMakes();
            WorkshopsByLocation();
        }

        private static void WorkshopsByLocation()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var workshopsByLocation = uow.Workshops.GetAll()
                    .GroupBy(loc => loc.Location, ws => ws, (location, workshops) => new
                    {
                        Location = location,
                        Workshops = workshops.Where(ws => ws.Participants.Count >= 5)
                    })
                    .Where(ws => ws.Workshops.Count() > 0)
                    .Select(wsByLocation => new
                    {
                        Location = wsByLocation.Location,
                        WorkShops = wsByLocation.Workshops
                    }
                    );

                var xml = new XElement("locations");
                foreach (var location in workshopsByLocation)
                {
                    var locationNode = new XElement("location");
                    locationNode.Add(new XAttribute("name", location.Location));
                    foreach (var ws in location.WorkShops)
                    {
                        var wsNode = new XElement("workshop");
                        wsNode.Add(new XAttribute("name", ws.Name));
                        wsNode.Add(new XAttribute("total-profit", CalculateTotalProfit(ws)));
                        var participantsNode = new XElement("participants");
                        participantsNode.Add(new XAttribute("count", ws.Participants.Count));
                        foreach (var participant in ws.Participants)
                        {
                            var participantNode = new XElement("participant");
                            participantNode.Value = participant.FullName;
                            participantsNode.Add(participantNode);
                        }
                        wsNode.Add(participantsNode);
                        locationNode.Add(wsNode);
                    }
                    xml.Add(locationNode);
                }
                xml.Save("../../workshops-by-location.xml");
            }
        }

        private static object CalculateTotalProfit(Workshop ws)
        {
            var totalProfit = (ws.Participants.Count * ws.PricePerParticipant) - ((ws.Participants.Count * ws.PricePerParticipant) * 0.2m);
            return totalProfit;
        }

        private static void PhotographersWithSameCameraMakes()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var photographers = uow.Photographers
                    .Find(ph => ph.PrimaryCamera.Make == ph.SecondaryCamera.Make);
                var xml = new XElement("photographers");
                foreach (var ph in photographers)
                {
                    var photographerNode = new XElement("photographer");
                    photographerNode.Add(new XAttribute("name", $"{ph.FirstName} {ph.LastName}"));
                    photographerNode.Add(new XAttribute("primary-camera", $"{ph.PrimaryCamera.Make} {ph.PrimaryCamera.Model}"));

                    if (ph.Lenses.Count > 0)
                    {
                        var lenses = ph.Lenses.Select(l => $"{l.Make} {l.FocalLength}mm f{l.MaxAperture}");
                        var lensesNode = new XElement("lenses");
                        foreach (var lens in lenses)
                        {
                            var lensNode = new XElement("lens");
                            lensNode.Value = lens;
                            lensesNode.Add(lensNode);
                        }
                        photographerNode.Add(lensesNode);
                    }
                    xml.Add(photographerNode);
                }
                xml.Save("../../same-cameras-photographers.xml");
            }
        }
    }
}
