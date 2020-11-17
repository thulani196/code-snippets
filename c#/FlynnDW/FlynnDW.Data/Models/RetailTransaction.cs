using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "RetailTransaction", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class RetailTransaction
	{
		[XmlElement(ElementName = "SpecialOrderNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string SpecialOrderNumber { get; set; }

		[XmlElement(ElementName = "PriceDerivationResult", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public PriceDerivationResult PriceDerivationResult { get; set; }

		[XmlElement(ElementName = "LineItem", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public List<LineItem> LineItem { get; set; }

		[XmlElement(ElementName = "Total", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public List<Total> Total { get; set; }

		[XmlElement(ElementName = "Customer", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string Customer { get; set; }

		[XmlElement(ElementName = "Foodservice", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Foodservice Foodservice { get; set; }

		[XmlAttribute(AttributeName = "TransactionStatus")]
		public string TransactionStatus { get; set; }
	}
}
