namespace ImportManufacturersFromXml
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using DatabaseFirstPhotographySystem;

    public class ImportManufacturers
    {
        private const string XmlPath = "../../manufacturers-and-goods.xml";

        static void Main()
        {
            var context = new PhotographySystemEntities();
            var xml = XDocument.Load(XmlPath);
            var xManufacturers = xml.XPathSelectElements("manufacturers/manufacturer");
            foreach (var xManufacturer in xManufacturers)
            {
                var manufacturer = new Manufacturer();

                var manufacturerName = xManufacturer.Attribute("name").Value;
                var exists = context.Manufacturers.Any(m => m.Name == manufacturerName);
                if (exists)
                {
                    Console.WriteLine("Manufacturer {0} already exists.",
                        manufacturerName);
                    continue;
                }

                manufacturer.Name = manufacturerName;

                var xCameras = xManufacturer.XPathSelectElements("cameras/camera");
                foreach (var xCamera in xCameras)
                {
                    var camera = new Camera();
                    camera.Model = xCamera.Attribute("model").Value;
                    camera.Year = int.Parse(xCamera.Attribute("year").Value);

                    if (xCamera.Attribute("megapixels") != null)
                    {
                        camera.Megapixels = int.Parse(xCamera.Attribute("megapixels").Value);
                    }

                    if (xCamera.Attribute("price") != null)
                    {
                        camera.Price = decimal.Parse(xCamera.Attribute("price").Value);
                    }

                    manufacturer.Cameras.Add(camera);
                }

                var xLenses = xManufacturer.XPathSelectElements("lenses/lens");
                foreach (var xLens in xLenses)
                {
                    var lens = new Lens();
                    lens.Model = xLens.Attribute("model").Value;
                    lens.Type = xLens.Attribute("type").Value;

                    if (xLens.Attribute("price") != null)
                    {
                        lens.Price = decimal.Parse(xLens.Attribute("price").Value);
                    }

                    manufacturer.Lenses.Add(lens);
                }

                context.Manufacturers.Add(manufacturer);
                context.SaveChanges();
                Console.WriteLine("Successfully added manufacturer {0}.",
                    manufacturerName);
            }
        }
    }
}
