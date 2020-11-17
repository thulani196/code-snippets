using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "PriceDerivationRule", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class PriceDerivationRule
    {
        [XmlElement(ElementName = "PriceDerivationRuleID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string PriceDerivationRuleID { get; set; }

        [XmlElement(ElementName = "Amount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public Amount Amount { get; set; }
    }
}
