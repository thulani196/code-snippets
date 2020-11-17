using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Modification", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Modification
    {
        [XmlElement(ElementName = "Name", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }
    }
}
