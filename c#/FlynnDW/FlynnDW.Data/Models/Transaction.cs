using System.Collections.Generic;
using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
	[XmlRoot(ElementName = "Transaction", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
	public class Transaction
	{
		[XmlElement(ElementName = "RetailStoreID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string RetailStoreID { get; set; }

		[XmlElement(ElementName = "OrganizationHierarchy", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public List<OrganizationHierarchy> OrganizationHierarchy { get; set; }

		[XmlElement(ElementName = "WorkstationID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string WorkstationID { get; set; }

		[XmlElement(ElementName = "SequenceNumber", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string SequenceNumber { get; set; }

		[XmlElement(ElementName = "BusinessDayDate", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string BusinessDayDate { get; set; }

		[XmlElement(ElementName = "BeginDateTime", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string BeginDateTime { get; set; }

		[XmlElement(ElementName = "EndDateTime", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string EndDateTime { get; set; }

		[XmlElement(ElementName = "OperatorID", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public OperatorID OperatorID { get; set; }

		[XmlElement(ElementName = "TrailerText", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public TrailerText TrailerText { get; set; }

		[XmlElement(ElementName = "ReceiptDateTime", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public string ReceiptDateTime { get; set; }

		[XmlElement(ElementName = "RetailTransaction", Namespace = "http://www.nrf-arts.org/IXRetail/namespace/")]
		public RetailTransaction RetailTransaction { get; set; }

		[XmlAttribute(AttributeName = "MajorVersion")]
		public string MajorVersion { get; set; }

		[XmlAttribute(AttributeName = "MinorVersion")]
		public string MinorVersion { get; set; }

		[XmlAttribute(AttributeName = "FixVersion")]
		public string FixVersion { get; set; }

		[XmlAttribute(AttributeName = "TrainingModeFlag")]
		public string TrainingModeFlag { get; set; }
	}
}
