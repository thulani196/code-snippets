using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "OrganizationHierarchy", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class OrganizationHierarchy
    {
        [XmlAttribute(AttributeName = "Level")]
        public string Level { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
