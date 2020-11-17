using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "BusinessUnit", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class BusinessUnit
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "TypeCode")]
        public string TypeCode { get; set; }
    }
}
