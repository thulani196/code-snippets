using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Total", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Total
    {
        [XmlAttribute(AttributeName = "TotalType")]
        public string TotalType { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
