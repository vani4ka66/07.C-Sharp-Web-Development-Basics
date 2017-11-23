namespace MassDeffect.ExportJson
{
    using System.IO;
    using System.Linq;
    using MassDeffect.Data;
    using Newtonsoft.Json;

    class Program
    {
        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            ExportPlanetWhichAreNotAnomalyOrigins(unit);
            ExportPeopleWhichHaveNotBeenVictims(unit);
            ExportTopAnomaly(unit);
        }

        private static void ExportTopAnomaly(UnitOfWork unit)
        {
            var anomaly = unit.Anomalies.GetAll().OrderByDescending(anomaly1 => anomaly1.Victims.Count).Take(1).Select(
                anomaly1 => new
                {
                    id = anomaly1.Id,
                    originPlanet = new
                    {
                        name = anomaly1.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        name = anomaly1.TeleportPlanet.Name
                    },
                    victimsCount = anomaly1.Victims.Count
                });

            string json = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
            File.WriteAllText("../../../results/topAnomaly.json", json);
        }

        private static void ExportPeopleWhichHaveNotBeenVictims(UnitOfWork unit)
        {
            var people = unit.Persons
                .GetAll(person => person.Anomalies.Count == 0)
                .Select(person =>
                new
                {
                    person.Name,
                    homePlanet = new
                    {
                        person.HomePlanet.Name
                    }
                });

            string json = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText("../../../results/peopleNotVictims.json", json);

        }

        private static void ExportPlanetWhichAreNotAnomalyOrigins(UnitOfWork unit)
        {
            var planets = unit.Planets.GetAll(planet => planet.OriginOfAnomalies.Count == 0).Select(planet => planet.Name);
            string json = JsonConvert.SerializeObject(planets, Formatting.Indented);
            File.WriteAllText("../../../results/planetsNotOrigin.json", json);
        }
    }
}
