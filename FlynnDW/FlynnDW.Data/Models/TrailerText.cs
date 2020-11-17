using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "TrailerText", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class TrailerText
    {
        [XmlElement(ElementName = "Text", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string Text { get; set; }
    }
}
