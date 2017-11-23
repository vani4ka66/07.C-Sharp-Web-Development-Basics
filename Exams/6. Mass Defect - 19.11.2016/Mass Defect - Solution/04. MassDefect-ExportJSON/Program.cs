using System.IO;
using System.Linq;
using _01.MassDefect_CodeFirst;
using Newtonsoft.Json;

namespace _04.MassDefect_ExportJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MassDefectContext();

            ExportPlanetsWhichAreNotAnomalyOrigins(context);

            ExportPeopleWhichHaveNotBeenVictims(context);

            ExportTopAnomaly(context);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(planet => !planet.OriginAnomalies.Any())
                .Select(planet => new
                {
                    name = planet.Name
                });

            var planetsAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText("../../planets.json", planetsAsJson);
        }

        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectContext context)
        {
            var exportedPeople = context.Persons
                .Where(person => !person.Anomalies.Any())
                .Select(person => new 
                {
                    name = person.Name,
                    homePlanet = new {
                        name = person.HomePlanet.Name
                    }
                });

            var peopleAsJson = JsonConvert.SerializeObject(exportedPeople, Formatting.Indented);
            File.WriteAllText("../../people.json", peopleAsJson);
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            var exportedAnomaly = context.Anomalies
                .OrderByDescending(anomaly => anomaly.Victims.Count)
                .Take(1)
                .Select(anomaly => new
                {
                    id = anomaly.Id,
                    originPlanet = new
                    {
                        name = anomaly.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        name = anomaly.TeleportPlanet.Name
                    },
                    victimsCount = anomaly.Victims.Count
                });

            var anomalyAsJson = JsonConvert.SerializeObject(exportedAnomaly, Formatting.Indented);
            File.WriteAllText("../../anomaly.json", anomalyAsJson);
        }
    }
}
