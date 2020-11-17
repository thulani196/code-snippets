using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "CreditDebit", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class CreditDebit
    {
        [XmlElement(ElementName = "PrimaryAccountNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string PrimaryAccountNumber { get; set; }
    }
}
