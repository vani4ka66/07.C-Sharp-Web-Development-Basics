using System;
using System.Xml.Linq;
using System.Xml.XPath;
using _01.MassDefect_CodeFirst;
using _01.MassDefect_CodeFirst.Models;
using _01.MassDefect_CodeFirst.Utilities;

namespace _03.MassDefect_ImportXML
{
    class Program
    {
        private const string NewAnomaliesPath = "../../../datasets/new-anomalies.xml";

        static void Main(string[] args)
        {
            var xml = XDocument.Load(NewAnomaliesPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");

            var context = new MassDefectContext();
            foreach (var anomaly in anomalies)
            {
                ImportAnomalyAndVictims(anomaly, context);
            }
        }

        private static void ImportAnomalyAndVictims(XElement anomalyNode, MassDefectContext context)
        {
            var originPlanetName = anomalyNode.Attribute("origin-planet");
            var teleportPlanetName = anomalyNode.Attribute("teleport-planet");

            if (originPlanetName == null || teleportPlanetName == null)
            {
                Console.WriteLine(Constants.ImportErrorMessage);
                return;
            }

            var anomalyEntity = new Anomaly
            {
                OriginPlanet = GetPlanetByName(originPlanetName.Value, context),
                TeleportPlanet = GetPlanetByName(teleportPlanetName.Value, context)
            };

            if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
            {
                Console.WriteLine(Constants.ImportErrorMessage);
                return;
            }

            context.Anomalies.Add(anomalyEntity);
            Console.WriteLine(Constants.ImportUnnamedEntitySuccessMessage);

            var victims = anomalyNode.XPathSelectElements("victims/victim");
            foreach (var victim in victims)
            {
                ImportVictim(victim, context, anomalyEntity);
            }

            context.SaveChanges();
        }

        private static void ImportVictim(XElement victimNode, MassDefectContext context, Anomaly anomaly)
        {
            var name = victimNode.Attribute("name");

            //check...

            var personEntity = GetPersonByName(name.Value, context);

            //check...

            anomaly.Victims.Add(personEntity);
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
    }
}
