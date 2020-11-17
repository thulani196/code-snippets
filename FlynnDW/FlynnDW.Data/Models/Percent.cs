using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Percent", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Percent
    {
        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
