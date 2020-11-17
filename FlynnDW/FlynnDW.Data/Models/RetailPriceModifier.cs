using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "RetailPriceModifier", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class RetailPriceModifier
	{
		[XmlElement(ElementName = "SequenceNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string SequenceNumber { get; set; }
		[XmlElement(ElementName = "Amount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Amount Amount { get; set; }
		[XmlElement(ElementName = "PromotionID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string PromotionID { get; set; }
		[XmlElement(ElementName = "ReasonCode", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string ReasonCode { get; set; }
		[XmlElement(ElementName = "RetailPriceModifierExtension", Namespace = "YUMArtsExtensions")]
		public RetailPriceModifierExtension RetailPriceModifierExtension { get; set; }
		[XmlAttribute(AttributeName = "DiscountBenefit")]
		public string DiscountBenefit { get; set; }
		[XmlAttribute(AttributeName = "MethodCode")]
		public string MethodCode { get; set; }
	}
}
