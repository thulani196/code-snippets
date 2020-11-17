using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "LineItem", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class LineItem
	{
		[XmlElement(ElementName = "SequenceNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string SequenceNumber { get; set; }

		[XmlElement(ElementName = "Sale", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public Sale Sale { get; set; }

		[XmlAttribute(AttributeName = "CancelFlag")]
		public string CancelFlag { get; set; }

		[XmlAttribute(AttributeName = "VoidFlag")]
		public string VoidFlag { get; set; }

		[XmlElement(ElementName = "YUMBOHTransactionExtensions", Namespace = "YUMArtsExtensions")]
		public YUMBOHTransactionExtensions YUMBOHTransactionExtensions { get; set; }

		[XmlElement(ElementName = "BeginDateTime", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string BeginDateTime { get; set; }

		[XmlElement(ElementName = "EndDateTime", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string EndDateTime { get; set; }

		[XmlElement(ElementName = "Tender", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public List<Tender> Tender { get; set; }
	}
}
