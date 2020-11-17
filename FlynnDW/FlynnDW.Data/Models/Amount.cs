using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Amount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Amount
    {
        [XmlAttribute(AttributeName = "Action")]
        public string Action { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
