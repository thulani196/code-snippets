using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "OrderCount", Namespace = "YUMArtsExtensions")]
    public class OrderCount
    {
        [XmlElement(ElementName = "Type", Namespace = "YUMArtsExtensions")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Count", Namespace = "YUMArtsExtensions")]
        public string Count { get; set; }
    }
}
