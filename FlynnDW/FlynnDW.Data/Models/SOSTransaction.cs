using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "SOSTransaction", Namespace = "YUMArtsExtensions")]
	public class SOSTransaction
	{
		[XmlElement(ElementName = "BusinessDate", Namespace = "YUMArtsExtensions")]
		public string BusinessDate { get; set; }

		[XmlElement(ElementName = "DayPart", Namespace = "YUMArtsExtensions")]
		public string DayPart { get; set; }

		[XmlElement(ElementName = "Event", Namespace = "YUMArtsExtensions")]
		public List<Event> Event { get; set; }

		[XmlAttribute(AttributeName = "TransactionType")]
		public string TransactionType { get; set; }
	}
}
