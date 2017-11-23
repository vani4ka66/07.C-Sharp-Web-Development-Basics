namespace _02.ImportXML
{
    using PhotographyWorkshop.Data;
    using PhotographyWorkshop.Models;
    using PhotographyWorkshop.Utilities;
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    class ImportXml
    {
        static void Main()
        {
            ImportXMLData();
        }

        static void ImportXMLData()
        {
            ImportAccessories();
            ImportWorkshops();
        }

        private static void ImportAccessories()
        {
            var xml = XDocument.Load(Constants.AccessoriesPath);
            var accesories = xml.XPathSelectElements("accessories/accessory");
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                foreach (var accessory in accesories)
                {
                    var acc = new Accessory()
                    {
                        Name = accessory.Attribute("name").Value
                    };
                    uow.Accesories.Add(acc);
                    Console.WriteLine($"Succesfully imported {acc.Name}");
                    uow.Commit();
                }
            }
        }

        private static void ImportWorkshops()
        {
            var xml = XDocument.Load(Constants.WorkshopsPath);
            var workshops = xml.XPathSelectElements("workshops/workshop");
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                foreach (var ws in workshops)
                {
                    var name = GetAttribute(ws, "name");
                    var location = GetAttribute(ws, "location");
                    var trainerName = ws.XPathSelectElement("trainer");
                    var price = GetAttribute(ws, "price");
                    if (Check.ForNull(name, location, trainerName, price))
                    {
                        Console.WriteLine(Messages.ErrorInvalidDataProvided);
                        continue;
                    }

                    var trainer = uow.Photographers
                        .Find(ph => ph.FirstName + " " + ph.LastName == trainerName.Value)
                        .FirstOrDefault();

                    DateTime? startDate = GetDateOrNull(ws, "start-date");
                    DateTime? endDate = GetDateOrNull(ws, "end-date");

                    var workshop = new Workshop()
                    {
                        Name = name.Value,
                        StartDate = startDate,
                        EndDate = endDate,
                        Location = location.Value,
                        PricePerParticipant = decimal.Parse(price.Value),
                        Trainer = trainer
                    };

                    var participants = ws.XPathSelectElements("participants/participant");
                    if (Check.ForNotNull(participants))
                    {
                        foreach (var p in participants)
                        {
                            string firstName = p.Attribute("first-name").Value;
                            string lastName = p.Attribute("last-name").Value;
                            var participant = uow.Photographers
                                .Find(ph => ph.FirstName == firstName && ph.LastName == lastName)
                                .FirstOrDefault();
                            workshop.Participants.Add(participant);
                        }
                    }

                    try
                    {
                        uow.Workshops.Add(workshop);
                        uow.Commit();
                        Console.WriteLine($"Succesfully imported {workshop.Name}");
                    }
                    catch (DbEntityValidationException)
                    {
                        uow.Workshops.Remove(workshop);
                        Console.WriteLine(Messages.ErrorInvalidDataProvided);
                    }
                }
            }
        }
        private static XAttribute GetAttribute(XElement element, string attributeName)
        {
            return element.Attributes().FirstOrDefault(a => a.Name == attributeName);
        }
        private static DateTime? GetDateOrNull(XElement ws, string attributeName)
        {
            DateTime? date = null;
            if (Check.ForNotNull(GetAttribute(ws, attributeName)))
            {
                date = DateTime.Parse(ws.Attribute(attributeName).Value);
            }

            return date;
        }
    }
}
