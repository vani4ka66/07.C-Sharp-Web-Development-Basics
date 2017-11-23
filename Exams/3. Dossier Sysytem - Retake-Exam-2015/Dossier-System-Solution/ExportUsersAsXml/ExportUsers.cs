namespace ExportUsersAsXml
{
    using System.Linq;
    using System.Xml.Linq;
    using DatabaseFirstPhotographySystem;

    public class ExportUsers
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            var usersData = context.Users
                .Where(u => u.Albums.Any())
                .OrderBy(u => u.FullName)
                .Select(u => new
                {
                    u.Id,
                    Albums = u.Albums
                        .Select(a => new
                        {
                            a.Name,
                            a.Description,
                            Photographs = a.Photographs
                                .Select(p => p.Title)
                        }),
                    u.BirthDate,
                    Camera = new
                    {
                        u.Equipment.Camera.Model,
                        u.Equipment.Camera.Megapixels,
                        Lens = u.Equipment.Lens.Model
                    }  
                });

            XElement root = new XElement("users");
            foreach (var user in usersData)
            {
                var xUser = new XElement("user");
                xUser.Add(new XAttribute("id", user.Id));
                xUser.Add(new XAttribute("birth-date", user.BirthDate));

                var xAlbumCollection = new XElement("albums");
                foreach (var album in user.Albums)
                {
                    var xAlbum = new XElement("album");
                    xAlbum.Add(new XAttribute("name", album.Name));
                    if (album.Description != null)
                    {
                        xAlbum.Add(new XAttribute("description", album.Description));
                    }

                    var xPhotographCollection = new XElement("photographs");
                    foreach (var photograph in album.Photographs)
                    {
                        var xPhotograph = new XElement("photograph");
                        xPhotograph.Add(new XAttribute("title", photograph));

                        xPhotographCollection.Add(xPhotograph);
                    }

                    xAlbum.Add(xPhotographCollection);
                    xAlbumCollection.Add(xAlbum);
                }

                xUser.Add(xAlbumCollection);
                
                var xCamera = new XElement("camera");
                xCamera.Add(new XAttribute("model", user.Camera.Model));
                xCamera.Add(new XAttribute("lens", user.Camera.Lens));
                xCamera.Add(new XAttribute("megapixels", user.Camera.Megapixels));

                xUser.Add(xCamera);
                root.Add(xUser);
            }
            
            root.Save("users.xml");
        }
    }
}
