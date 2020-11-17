using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "OperatorID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class OperatorID
    {
        [XmlAttribute(AttributeName = "OperatorType")]
        public string OperatorType { get; set; }

        [XmlAttribute(AttributeName = "OperatorName")]
        public string OperatorName { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
