namespace MassDeffect.XmlImport
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using MassDeffect.Data;
    using MassDeffect.Models;

    class Program
    {
        private static string AnomaliesPath = "../../../datasets/new-anomalies.xml";
        private static string Error = "Error: Invalid data.";

        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            ImportAnomalies(unit);
        }

        private static void ImportAnomalies(UnitOfWork unit)
        {
            XDocument document = XDocument.Load(AnomaliesPath);
            var anomaliesXmls = document.Descendants("anomaly");
            foreach (XElement anomalyXml in anomaliesXmls)
            {
                var originPlanetAttr = anomalyXml.Attribute("origin-planet");
                var teleportPlanetAttr = anomalyXml.Attribute("teleport-planet");

                if (originPlanetAttr == null || teleportPlanetAttr == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                Planet originPlanet = unit.Planets.First(planet => planet.Name == originPlanetAttr.Value);
                Planet teleportPlanet = unit.Planets.First(planet => planet.Name == teleportPlanetAttr.Value);

                if (originPlanet == null || teleportPlanet == null)
                {
                    Console.WriteLine(Error);
                    continue;
                }

                var victimsXmls = anomalyXml.Descendants("victim");
                List<Person> victims = new List<Person>();
                foreach (XElement victimXml in victimsXmls)
                {
                    var victimNameAttr = victimXml.Attribute("name");
                    if (victimNameAttr == null)
                    {
                        Console.WriteLine(Error);
                        continue;
                    }

                    Person person = unit.Persons.First(person1 => person1.Name == victimNameAttr.Value);
                    if (person == null)
                    {
                        Console.WriteLine(Error);
                        continue;
                    }

                    victims.Add(person);
                }

                Anomaly anomaly = new Anomaly()
                {
                    OriginPlanet = originPlanet,
                    TeleportPlanet = teleportPlanet,
                    Victims = victims
                };

                unit.Anomalies.Add(anomaly);
                unit.Commit();
                Console.WriteLine("Successfully added anomaly!");
            }
        }
    }
}
