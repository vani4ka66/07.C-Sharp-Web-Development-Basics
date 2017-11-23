namespace _02.Export_Albums_As_Json
{
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using DatabaseFirstPhotographySystem;

    public class ExportAlbums
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            var albums = context.Albums
                .Where(a => a.Photographs.Count > 1)
                .OrderBy(a => a.Photographs.Count)
                .ThenBy(a => a.Id)
                .Select(a => new
                {
                    id = a.Id,
                    name = a.Name,
                    owner = a.User.FullName,
                    photoCount = a.Photographs.Count
                });

            
            var result = JsonConvert.SerializeObject(albums, Formatting.Indented);
            File.WriteAllText("albums.json", result);    
        }
    }
}
