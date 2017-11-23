namespace DossierSystemQueries
{
    using System;
    using System.IO;
    using System.Linq;
    using EntityFramework.Extensions;
    using Newtonsoft.Json;
    using DossierSystem;
    using DossierSystem.Models;

    public class CodeFirstQueries
    {
        static void Main()
        {
            var context = new DossierContext();

            ExportActiveIndividuals(context);
            ExportDrugActivities(context);
            ExportOsloVisitors(context);
            UpdateMissingIndividuals(context);
        }

        private static void UpdateMissingIndividuals(DossierContext context)
        {
            var updatedRows = context.Individuals
                 .Where(i => !i.Locations
                     .Any(l => l.LastSeen.Year >= 2010) && i.Status == Status.Active)
                 .Update(i => new Individual { Status = Status.Missing });

            Console.WriteLine(updatedRows);
        }

        private static void ExportOsloVisitors(DossierContext context)
        {
            var data = context.Individuals
                .Where(i => i.Locations
                    .Any(l => l.City.Name == "Jurish" && l.LastSeen.Year < 2005))
                .Select(i => new
                {
                    fullName = i.FullName,
                    locations = i.Locations
                        .OrderBy(l => l.LastSeen)
                        .Select(l => new
                        {
                            seen = l.LastSeen,
                        })
                });

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("oslo-visitors.json", json);
        }

        private static void ExportDrugActivities(DossierContext context)
        {
            var activities = context.Individuals
                .Where(i => i.Activities
                    .Any(a => a.ActivityType.Name == "DrugTrafficking"))
                .OrderBy(i => i.FullName)
                .Select(i => new
                {
                    fullName = i.FullName,
                    birthDate = i.BirthDate,
                    activities = i.Activities
                        .Where(a => a.ActivityType.Name == "DrugTrafficking")
                        .OrderBy(a => a.ActiveFrom)
                        .Select(a => new
                        {
                            description = a.Description,
                            activeFrom = a.ActiveFrom,
                            activeTo = a.ActiveTo
                        })
                });

            var json = JsonConvert.SerializeObject(activities, Formatting.Indented);
            File.WriteAllText("drug-activities.json", json);
        }

        private static void ExportActiveIndividuals(DossierContext context)
        {
            var activeIndividuals = context.Individuals
                .Where(i => i.Status == Status.Missing)
                .OrderBy(i => i.FullName)
                .Select(i => new
                {
                    id = i.Id,
                    fullName = i.FullName,
                    alias = i.Alias
                })
                .ToList();

            var json = JsonConvert.SerializeObject(activeIndividuals, Formatting.Indented);
            File.WriteAllText("active-individuals.json", json);
        }
    }
}
