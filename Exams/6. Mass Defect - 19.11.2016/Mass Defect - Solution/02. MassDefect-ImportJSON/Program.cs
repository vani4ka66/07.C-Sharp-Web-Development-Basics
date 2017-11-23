using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using _01.MassDefect_CodeFirst;
using _01.MassDefect_CodeFirst.Models.DTO;
using Newtonsoft.Json;
using _01.MassDefect_CodeFirst.Models;
using _01.MassDefect_CodeFirst.Utilities;

namespace _02.MassDefect_ImportJSON
{
    class Program
    {
        private const string SolarSystemsPath = "../../../datasets/solar-systems.json";

        private const string StarsPath = "../../../datasets/stars.json";

        private const string PlanetsPath = "../../../datasets/planets.json";

        private const string PersonsPath = "../../../datasets/persons.json";

        private const string AnomaliesPath = "../../../datasets/anomalies.json";

        private const string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";

        static void Main(string[] args)
        {
            ImportSolarSystems();
            ImportStars();
            ImportPlanets();
            ImportPersons();
            ImportAnomalies();
            ImportAnomalyVictims();
        }

        private static void ImportSolarSystems()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(SolarSystemsPath);
            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);

            foreach (var solarSystem in solarSystems)
            {
                if (solarSystem.Name == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var solarSystemEntity = new SolarSystem
                {
                    Name = solarSystem.Name
                };

                context.SolarSystems.Add(solarSystemEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Solar System", solarSystemEntity.Name);
            }

            context.SaveChanges();
        }

        private static void ImportStars()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(StarsPath);
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

            foreach (var star in stars)
            {
                if (star.Name == null || star.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var starEntity = new Star
                {
                    Name = star.Name,
                    SolarSystem = GetSolarSystemByName(star.SolarSystem, context)
                };

                if (starEntity.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }
                
                context.Stars.Add(starEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Star", starEntity.Name);
            }

            context.SaveChanges();
        }

        private static void ImportPlanets()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(PlanetsPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

            foreach (var planet in planets)
            {
                if (planet.Name == null || planet.Sun == null || planet.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var planetEntity = new Planet
                {
                    Name = planet.Name,
                    Sun = GetStarByName(planet.Sun, context),
                    SolarSystem = GetSolarSystemByName(planet.SolarSystem, context)
                };

                if (planetEntity.Sun == null || planetEntity.SolarSystem == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Planets.Add(planetEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Planets", planetEntity.Name);
            }

            context.SaveChanges();
        }

        private static void ImportPersons()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(PersonsPath);
            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);

            foreach (var person in persons)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var personEntity = new Person
                {
                    Name = person.Name,
                    HomePlanet = GetPlanetByName(person.HomePlanet, context)
                };

                if (personEntity.HomePlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Persons.Add(personEntity);
                Console.WriteLine(Constants.ImportNamedEntitySuccessMessage, "Person", personEntity.Name);
            }

            context.SaveChanges();
        }

        private static void ImportAnomalies()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(AnomaliesPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);

            foreach (var anomaly in anomalies)
            {
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var anomalyEntity = new Anomaly
                {
                    OriginPlanet = GetPlanetByName(anomaly.OriginPlanet, context),
                    TeleportPlanet = GetPlanetByName(anomaly.TeleportPlanet, context)
                };

                if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                context.Anomalies.Add(anomalyEntity);
                Console.WriteLine(Constants.ImportUnnamedEntitySuccessMessage);
            }

            context.SaveChanges();
        }
        private static void ImportAnomalyVictims()
        {
            var context = new MassDefectContext();
            var json = File.ReadAllText(AnomalyVictimsPath);
            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(json);

            foreach (var anomalyVictim in anomalyVictims)
            {
                if (anomalyVictim.Id == null || anomalyVictim.Person == null)
                {
                    Console.WriteLine(Constants.ImportErrorMessage);
                    continue;
                }

                var anomalyEntity = GetAnomalyById(anomalyVictim.Id, context);
                var personEntity = GetPersonByName(anomalyVictim.Person, context);

                if (anomalyEntity == null || personEntity == null)
                {
                    //error message here...
                    continue;
                }

                anomalyEntity.Victims.Add(personEntity);
            }

            context.SaveChanges();
        }

        private static SolarSystem GetSolarSystemByName(string solarSystemName, MassDefectContext context)
        {
            foreach (var solarSystem in context.SolarSystems)
            {
                if (solarSystem.Name == solarSystemName)
                {
                    return solarSystem;
                }
            }

            return null;
        }

        private static Star GetStarByName(string starName, MassDefectContext context)
        {
            foreach (var star in context.Stars)
            {
                if (star.Name == starName)
                {
                    return star;
                }
            }

            return null;
        }
        private static Planet GetPlanetByName(string planetName, MassDefectContext context)
        {
            foreach (var planet in context.Planets)
            {
                if (planet.Name == planetName)
                {
                    return planet;
                }
            }

            return null;
        }

        private static Person GetPersonByName(string personName, MassDefectContext context)
        {
            foreach (var person in context.Persons)
            {
                if (person.Name == personName)
                {
                    return person;
                }
            }

            return null;
        }

        private static Anomaly GetAnomalyById(int? anomalyId, MassDefectContext context)
        {
            foreach (var anomaly in context.Anomalies)
            {
                if (anomaly.Id == anomalyId)
                {
                    return anomaly;
                }
            }

            return null;
        }
    }
}
