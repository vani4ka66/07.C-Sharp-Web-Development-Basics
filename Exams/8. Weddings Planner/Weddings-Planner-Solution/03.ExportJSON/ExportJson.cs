using Newtonsoft.Json;
using System.IO;
using System.Linq;
using WeddingsPlanner.Data;
using WeddingsPlanner.Models.Enums;

namespace _03.ExportJSON
{
    class ExportJson
    {
        static void Main(string[] args)
        {
            ExportAgenciesOrdered();
            ExportGuestLists();

        }

        private static void ExportGuestLists()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {

                var weddings = uow.Weddings.GetAll()
                    .Select(w => new
                    {
                        bride = w.Bride.FullName,
                        bridegroom = w.Bridegroom.FullName,
                        agency = new { name = w.Agency.Name, town = w.Agency.Town },

                        invitedGuests = w.Invitations.Count,
                        brideGuests = w.Invitations.Where(i => i.Family == Family.Bride).Count(),
                        bridegroomGuests = w.Invitations.Where(i => i.Family == Family.Bridegroom).Count(),
                        attendingGuests = w.Invitations.Where(i => i.IsAttending).Count(),
                        guests = w.Invitations.Where(i => i.IsAttending).Select(i => i.Guest.FullName)
                    })
                    .OrderByDescending(w => w.invitedGuests)
                    .ThenBy(w => w.attendingGuests);

                string json = JsonConvert.SerializeObject(weddings, Formatting.Indented);

                File.WriteAllText("../../guests.json", json);
            }
        }

        private static void ExportAgenciesOrdered()
        {
            using (var uow = new UnitOfWork(new WeddingPlannerContext()))
            {
                var agencies = uow.Agencies.GetAll()
                    .OrderByDescending(a => a.EmployeesCount)
                    .ThenBy(a => a.Name)
                    .Select(a => new
                    {
                        name = a.Name,
                        count = a.EmployeesCount,
                        town = a.Town
                    });

                var json = JsonConvert.SerializeObject(agencies, Formatting.Indented);

                File.WriteAllText("../../agencies-ordered.json", json);
            }
        }
    }
}
