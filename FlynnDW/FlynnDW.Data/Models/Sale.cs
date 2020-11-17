using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "Sale", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class Sale
	{
		[XmlElement(ElementName = "POSIdentity", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public POSIdentity POSIdentity { get; set; }

		[XmlElement(ElementName = "RegularSalesUnitPrice", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string RegularSalesUnitPrice { get; set; }

		[XmlElement(ElementName = "ActualSalesUnitPrice", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string ActualSalesUnitPrice { get; set; }

		[XmlElement(ElementName = "ExtendedAmount", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string ExtendedAmount { get; set; }

		[XmlElement(ElementName = "Quantity", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string Quantity { get; set; }

		[XmlElement(ElementName = "Description", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string Description { get; set; }

		[XmlElement(ElementName = "ItemLink", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string ItemLink { get; set; }

		[XmlElement(ElementName = "RetailPriceModifier", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public RetailPriceModifier RetailPriceModifier { get; set; }

		[XmlElement(ElementName = "Item", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public List<Item> Item { get; set; }

		[XmlElement(ElementName = "Combo", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Combo Combo { get; set; }

		[XmlAttribute(AttributeName = "ItemType")]
		public string ItemType { get; set; }
	}
}
