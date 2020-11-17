using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "SpeedOfService", Namespace = "YUMArtsExtensions")]
    public class SpeedOfService
    {
        [XmlElement(ElementName = "OrderSpeedOfService", Namespace = "YUMArtsExtensions")]
        public OrderSpeedOfService OrderSpeedOfService { get; set; }
    }
}
