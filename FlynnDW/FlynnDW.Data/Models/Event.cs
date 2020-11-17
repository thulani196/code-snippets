using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "Event", Namespace = "YUMArtsExtensions")]
	public class Event
	{
		[XmlElement(ElementName = "EventID", Namespace = "YUMArtsExtensions")]
		public EventID EventID { get; set; }

		[XmlElement(ElementName = "BeginDateTime", Namespace = "YUMArtsExtensions")]
		public string BeginDateTime { get; set; }

		[XmlElement(ElementName = "EndDateTime", Namespace = "YUMArtsExtensions")]
		public string EndDateTime { get; set; }

		[XmlElement(ElementName = "DurationSeconds", Namespace = "YUMArtsExtensions")]
		public string DurationSeconds { get; set; }

		[XmlAttribute(AttributeName = "EventType")]
		public string EventType { get; set; }

		[XmlAttribute(AttributeName = "Location")]
		public string Location { get; set; }
	}
}
