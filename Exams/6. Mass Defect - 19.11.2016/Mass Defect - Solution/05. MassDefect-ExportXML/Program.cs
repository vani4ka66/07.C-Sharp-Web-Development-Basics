using System.Linq;
using System.Xml.Linq;
using _01.MassDefect_CodeFirst;

namespace _05.MassDefect_ExportXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MassDefectContext();
            var exportedAnomalies = context.Anomalies
                .Select(anomaly => new
                {
                    id = anomaly.Id,
                    originPlanetName = anomaly.OriginPlanet.Name,
                    teleportPlanetName = anomaly.TeleportPlanet.Name,
                    victims = anomaly.Victims.Select(victim => victim.Name)
                })
                .OrderBy(exportedAnomaly => exportedAnomaly.id);

            var xmlDocument = new XElement("anomalies");

            foreach (var exportedAnomaly in exportedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", exportedAnomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", exportedAnomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleport-planet", exportedAnomaly.teleportPlanetName));

                var victimsNode = new XElement("victims");

                foreach (var victim in exportedAnomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim));
                    victimsNode.Add(victimNode);    
                }
                
                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save("../../anomalies.xml");
        }
    }
}
