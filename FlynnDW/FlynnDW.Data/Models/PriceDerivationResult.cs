using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "PriceDerivationResult", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class PriceDerivationResult
	{
		[XmlElement(ElementName = "SequenceNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string SequenceNumber { get; set; }

		[XmlElement(ElementName = "Percent", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Percent Percent { get; set; }

		[XmlElement(ElementName = "PriceDerivationRule", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public PriceDerivationRule PriceDerivationRule { get; set; }

		[XmlElement(ElementName = "ReasonCode", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string ReasonCode { get; set; }

		[XmlElement(ElementName = "OperatorBypassApproval", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public OperatorBypassApproval OperatorBypassApproval { get; set; }

		[XmlAttribute(AttributeName = "DiscountBenefit")]
		public string DiscountBenefit { get; set; }
	}
}
