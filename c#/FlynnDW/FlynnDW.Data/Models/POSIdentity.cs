using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "POSIdentity", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class POSIdentity
    {
        [XmlElement(ElementName = "POSItemID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string POSItemID { get; set; }
    }
}
