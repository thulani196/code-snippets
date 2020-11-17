using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "POSLog", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class POSLog
	{
		[XmlElement(ElementName = "Batch", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Batch Batch { get; set; }

		[XmlElement(ElementName = "Transaction", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public List<Transaction> Transaction { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }

		[XmlAttribute(AttributeName = "ns1", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Ns1 { get; set; }

		[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
		public string SchemaLocation { get; set; }

		[XmlAttribute(AttributeName = "xyz", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xyz { get; set; }
	}
}
