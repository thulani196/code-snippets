using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models 
{ 
    [XmlRoot(ElementName = "OperatorBypassApproval", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
    public class OperatorBypassApproval
    {
        [XmlElement(ElementName = "SequenceNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string SequenceNumber { get; set; }

        [XmlElement(ElementName = "ApproverID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
        public string ApproverID { get; set; }
    }
}
