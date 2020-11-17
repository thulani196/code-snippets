using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Foodservice", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Foodservice
    {
        [XmlAttribute(AttributeName = "DestinationType")]
        public string DestinationType { get; set; }
    }
}
