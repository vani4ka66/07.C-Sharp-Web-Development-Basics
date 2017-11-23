using _01.ImportJSON.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using WeddingsPlanner.Data;
using WeddingsPlanner.Models;

namespace _01.ImportJSON
{
    class ImportJson
    {

        static void Main()
        {
            ImportData();

        }

        private static void ImportData()
        {
            ImportAgencies();
            ImportPeople();
            ImportWeddings();
        }

        private static void ImportAgencies()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {
                var json = File.ReadAllText("../../../datasets/agencies.json");
                var agencies = JsonConvert.DeserializeObject<IEnumerable<Agency>>(json);

                foreach (var agency in agencies)
                {
                    uow.Agencies.Add(agency);
                    uow.Commit();
                    Console.WriteLine($"Succesfully imported {agency.Name}");
                }
            }
        }
        private static void ImportPeople()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {
                var json = File.ReadAllText("../../../datasets/people.json");
                var peopleDto = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(json);
                foreach (var p in peopleDto)
                {
                    if (p.FirstName == null || p.LastName == null ||
                        p.MiddleInitial == null || p.MiddleInitial.Length != 1)
                    {
                        Console.WriteLine("Error. Invalid data provided");
                        continue;
                    }
                    var person = new Person()
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        MiddleNameInitial = p.MiddleInitial[0].ToString(),
                        Gender = p.Gender,
                        Birthdate = p.Birthday,
                        Phone = p.Phone,
                        Email = p.Email
                    };
                    try
                    {
                        uow.People.Add(person);
                        uow.Commit();
                        Console.WriteLine($"Succesfully imported {person.FullName}");
                    }
                    catch (DbEntityValidationException)
                    {
                        uow.People.Remove(person);
                        Console.WriteLine("Error. Invalid data provided");
                    }
                }
            }
        }
        private static void ImportWeddings()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {

                var json = File.ReadAllText("../../../datasets/weddings.json");
                var weddingsDto = JsonConvert.DeserializeObject<IEnumerable<WeddingDto>>(json);
                foreach (var w in weddingsDto)
                {
                    var bride = uow.People.Find(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == w.Bride).FirstOrDefault();
                    var bridegroom = uow.People.Find(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == w.Bridegroom).FirstOrDefault();
                    var agency = uow.Agencies.Find(a => a.Name == w.Agency).FirstOrDefault();
                    if (bride == null || bridegroom == null || w.Date == default(DateTime) || agency == null)
                    {
                        Console.WriteLine("Error. Invalid data provided");
                        continue;
                    }
                    var wedding = new Wedding()
                    {
                        Bride = bride,
                        Bridegroom = bridegroom,
                        Date = w.Date,
                        Agency = agency
                    };
                    if (w.Guests != null)
                    {
                        foreach (var g in w.Guests)
                        {
                            var guest = uow.People.Find(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == g.Name).FirstOrDefault();
                            if (guest != null)
                            {
                                wedding.Invitations.Add(new Invitation()
                                {
                                    Guest = guest,
                                    IsAttending = g.RSVP,
                                    Family = g.Family
                                });
                            }
                        }
                    }

                    try
                    {
                        uow.Weddings.Add(wedding);
                        uow.Commit();
                        Console.WriteLine($"Succesfully imported wedding of {bride.FirstName} and {bridegroom.FirstName}");
                    }
                    catch (DbEntityValidationException)
                    {
                        uow.Weddings.Remove(wedding);
                        Console.WriteLine("Error. Invalid data provided");
                    }
                }
            }

        }


    }
}
