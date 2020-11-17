using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "Combo", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class Combo
    {
        [XmlElement(ElementName = "Member", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public List<Member> Member { get; set; }
    }
}
