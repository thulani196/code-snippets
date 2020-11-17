using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "ActivitySummary", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class ActivitySummary
    {
        [XmlElement(ElementName = "TransactionCount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string TransactionCount { get; set; }

        [XmlElement(ElementName = "TransactionAmount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string TransactionAmount { get; set; }
    }
}
