using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "YUMBOHTransactionExtensions", Namespace = "YUMArtsExtensions")]
    public class YUMBOHTransactionExtensions
    {
        [XmlElement(ElementName = "SpeedOfService", Namespace = "YUMArtsExtensions")]
        public SpeedOfService SpeedOfService { get; set; }

        [XmlAttribute(AttributeName = "ReportingPerspectiveType")]
        public string ReportingPerspectiveType { get; set; }
    }
}
