using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Authorization", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Authorization
    {
        [XmlElement(ElementName = "AuthorizationCode", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string AuthorizationCode { get; set; }
    }
}
