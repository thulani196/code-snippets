using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Member", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Member
    {
        [XmlElement(ElementName = "SequenceNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string SequenceNumber { get; set; }

        [XmlElement(ElementName = "Sale", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public Sale Sale { get; set; }

        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }
    }
}
