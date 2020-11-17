using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "OrderSpeedOfService", Namespace = "YUMArtsExtensions")]
	public class OrderSpeedOfService
	{
		[XmlElement(ElementName = "OperatorID", Namespace = "YUMArtsExtensions")]
		public string OperatorID2 { get; set; }

		[XmlElement(ElementName = "OrderCount", Namespace = "YUMArtsExtensions")]
		public OrderCount OrderCount { get; set; }

		[XmlElement(ElementName = "SOSTransaction", Namespace = "YUMArtsExtensions")]
		public SOSTransaction SOSTransaction { get; set; }
	}
}
