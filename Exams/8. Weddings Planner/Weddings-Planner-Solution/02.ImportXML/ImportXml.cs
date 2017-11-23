namespace _02.ImportXML
{
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using WeddingsPlanner.Data;
    using WeddingsPlanner.Models;
    using WeddingsPlanner.Models.Enums;

    class ImportXml
    {
        static void Main(string[] args)
        {
            ImportData();
        }

        static void ImportData()
        {
            ImportVenues();
            ImportPresents();
        }
        private static void ImportVenues()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {
                var xml = XDocument.Load("../../../datasets/venues.xml");
                var venues = xml.XPathSelectElements("venues/venue");

                foreach (var v in venues)
                {
                    var name = v.Attribute("name").Value;
                    var capacity = int.Parse(v.XPathSelectElement("capacity").Value);
                    var town = v.XPathSelectElement("town").Value;
                    var venue = new Venue()
                    {
                        Name = name,
                        Town = town,
                        Capacity = capacity
                    };

                    uow.Venues.Add(venue);
                    uow.Commit();
                    Console.WriteLine($"Successfully imported {name}");
                }
                Random r = new Random();
                foreach (var w in uow.Weddings.GetAll())
                {
                    w.Venues.Add(uow.Venues.Get(r.Next(1, uow.Venues.GetAll().Count())));
                    w.Venues.Add(uow.Venues.Get(r.Next(1, uow.Venues.GetAll().Count() - 2)));
                }
                uow.Commit();
            }
        }

        private static void ImportPresents()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {

                var xml = XDocument.Load("../../../datasets/presents.xml");
                var presents = xml.XPathSelectElements("presents/present");
                foreach (var p in presents)
                {
                    var typeAtrribute = p.Attribute("type");
                    var invitationAttribute = p.Attribute("invitation-id");

                    if (typeAtrribute == null || invitationAttribute == null)
                    {
                        Console.WriteLine("Error. Invalid data provided type");
                        continue;
                    }
                    var invitationId = int.Parse(invitationAttribute.Value);
                    if (invitationId < 0 || invitationId > uow.Invitations.GetAll().Count())
                    {
                        Console.WriteLine("Error. Invalid data provided inv id");
                        continue;
                    }

                    if (typeAtrribute.Value == "cash")
                    {
                        var amountAttribute = p.Attribute("amount");
                        if (amountAttribute == null)
                        {
                            Console.WriteLine("Error. Invalid data provided ammount");
                            continue;
                        }
                        var cash = new Cash()
                        {
                            Amount = decimal.Parse(amountAttribute.Value)
                        };
                        var inv = uow.Invitations.Get(invitationId);
                        inv.Present = cash;
                        try
                        {
                            uow.Commit();
                            Console.WriteLine($"Succesfully imported gift from {inv.Guest.FullName}");
                        }
                        catch (DbEntityValidationException)
                        {
                            Console.WriteLine("Error. Invalid data provided amount");
                        }

                    }
                    else if (typeAtrribute.Value == "gift")
                    {
                        var nameAttribute = p.Attribute("present-name");
                        if (nameAttribute == null)
                        {
                            Console.WriteLine("Error. Invalid data provided gift name");
                            continue;
                        }
                        var sizeAttribute = p.Attribute("size");
                        var size = PresentSize.NotSpecified;
                        if (sizeAttribute != null)
                        {
                            if (!Enum.TryParse(sizeAttribute.Value, out size))
                            {
                                Console.WriteLine("Error. Invalid data provided size " + sizeAttribute.Value);
                                continue;
                            }
                        }
                        var gift = new Gift()
                        {
                            Name = nameAttribute.Value,
                            Size = size
                        };
                        var inv = uow.Invitations.Get(invitationId);
                        inv.Present = gift;
                        try
                        {
                            uow.Commit();
                            Console.WriteLine($"Succesfully imported gift from {inv.Guest.FullName}");
                        }
                        catch (DbEntityValidationException)
                        {
                            Console.WriteLine("Error. Invalid data provided");
                        }
                    }


                }
            }
        }
    }
}
