using Newtonsoft.Json;
using PhotographyWorkshop.Data;
using PhotographyWorkshop.Models;
using System;
using System.Linq;

namespace _03.ExportJSON
{
    class ExportJson
    {
        static void Main()
        {
            //PhotographersOrdered();
            //GetLandscapePhotographers();
        }

        private static void PhotographersOrdered()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var photographers = uow.Photographers.GetAll()
                    .OrderBy(ph => ph.FirstName)
                    .ThenByDescending(ph => ph.LastName)
                    .Select(ph => new
                    {
                        ph.FirstName,
                        ph.LastName,
                        ph.Phone
                    });
                var json = JsonConvert.SerializeObject(photographers, Formatting.Indented);
                Console.WriteLine(json);
                //TODO save it to file
            }
        }

        private static void GetLandscapePhotographers()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var photographers = uow.Photographers
                    .Find(ph => ph.PrimaryCamera is DslrCamera &&
                       ph.Lenses.Count > 0 &&
                       ph.Lenses.All(l => l.FocalLength <= 30))
                    .OrderBy(ph => ph.FirstName)
                    .Select(ph => new
                    {
                        ph.FirstName,
                        ph.LastName,
                        CameraMake = ph.PrimaryCamera.Make,
                        LensesCount = ph.Lenses.Count
                    }
                    );
                var json = JsonConvert.SerializeObject(photographers, Formatting.Indented);
                Console.WriteLine(json);
                //TODO save it to file
            }
        }
    }
}
