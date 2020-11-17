using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "Batch", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class Batch
	{
		[XmlElement(ElementName = "BatchID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string BatchID { get; set; }

		[XmlElement(ElementName = "BusinessUnit", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public BusinessUnit BusinessUnit { get; set; }

		[XmlElement(ElementName = "FirstTransactionTimestamp", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string FirstTransactionTimestamp { get; set; }

		[XmlElement(ElementName = "LastTransactionTimestamp", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string LastTransactionTimestamp { get; set; }

		[XmlElement(ElementName = "BatchCompleteTimestamp", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string BatchCompleteTimestamp { get; set; }

		[XmlElement(ElementName = "ActivitySummary", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public ActivitySummary ActivitySummary { get; set; }
	}
}
