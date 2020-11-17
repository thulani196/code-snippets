using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "Tender", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class Tender
	{
		[XmlElement(ElementName = "Amount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string Amount { get; set; }

		[XmlElement(ElementName = "Authorization", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Authorization Authorization { get; set; }

		[XmlElement(ElementName = "CreditDebit", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public CreditDebit CreditDebit { get; set; }

		[XmlElement(ElementName = "CustomTenderItem", Namespace = "YUMArtsExtensions")]
		public CustomTenderItem CustomTenderItem { get; set; }

		[XmlAttribute(AttributeName = "TenderType")]
		public string TenderType { get; set; }

		[XmlAttribute(AttributeName = "SubTenderType")]
		public string SubTenderType { get; set; }

		[XmlAttribute(AttributeName = "TenderStatus", Namespace = "YUMArtsExtensions")]
		public string TenderStatus { get; set; }
	}
}
