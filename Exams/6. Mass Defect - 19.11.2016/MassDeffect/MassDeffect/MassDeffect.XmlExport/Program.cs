namespace MassDeffect.XmlExport
{
    using System.Linq;
    using System.Xml.Linq;
    using MassDeffect.Data;

    class Program
    {
        static void Main()
        {
            UnitOfWork unit = new UnitOfWork();
            var exportedAnomalies = unit.Anomalies.GetAll().Select(anomaly => new
            {
                anomaly.Id,
                originPlanet = anomaly.OriginPlanet.Name,
                teleportPlanet = anomaly.TeleportPlanet.Name,
                victims = anomaly.Victims.Select(person => person.Name)
            });

            XElement root = new XElement("anomalies");
            foreach (var anomaly in exportedAnomalies)
            {
                XElement anomalyXml = new XElement("anomaly");
                anomalyXml.SetAttributeValue($"{nameof(anomaly.Id)}", anomaly.Id);
                anomalyXml.SetAttributeValue($"{nameof(anomaly.originPlanet)}", anomaly.originPlanet);
                anomalyXml.SetAttributeValue($"{nameof(anomaly.teleportPlanet)}", anomaly.teleportPlanet);
                XElement victimsXml = new XElement("victims");
                foreach (string victimName in anomaly.victims)
                {
                    XElement victimXml = new XElement("victim");
                    victimXml.SetAttributeValue("name", victimName);
                    victimsXml.Add(victimXml);
                }

                anomalyXml.Add(victimsXml);
                root.Add(anomalyXml);
            }

            root.Save("../../../results/anomalies.xml");
        }
    }
}
