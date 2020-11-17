using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "Item", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class Item
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
		[XmlAttribute(AttributeName = "Action")]
		public string Action { get; set; }
		[XmlElement(ElementName = "Modification", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Modification Modification { get; set; }
	}
}
